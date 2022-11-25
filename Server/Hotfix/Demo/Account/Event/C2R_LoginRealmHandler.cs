using System;

namespace ET
{
    public class C2R_LoginRealmHandler: AMRpcHandler<C2R_LoginRealm, R2C_LoginRealm>
    {
        protected override async ETTask Run(Session session, C2R_LoginRealm request, R2C_LoginRealm response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Realm)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{session.DomainScene().SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                session.Dispose();
                return;
            }

            // 防止重复请求
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RepeatedRequestError;
                reply();
                return;
            }

            var token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session?.Disconnect().Coroutine();
                return;
            }
            
            //不需要再保留之前的Token
            session.DomainScene().GetComponent<TokenComponent>().Remove(request.AccountId);

            using (session.AddComponent<SessionLockingComponent>())
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginRealm, request.AccountId))
            {
                //获取一个随机Gate 目前只有1个
                StartSceneConfig gateConfig = RealmGateAddressHelper.GetGate(session.DomainZone());
                G2R_GetLoginGateKey g2RGetLoginGateKey = (G2R_GetLoginGateKey)await MessageHelper.CallActor(gateConfig.InstanceId,
                    new R2G_GetLoginGateKey() { AccountId = request.AccountId });

                if (g2RGetLoginGateKey.Error != ErrorCode.ERR_Success)
                {
                    Log.Error(g2RGetLoginGateKey.Error.ToString());
                    reply();
                    session?.Disconnect().Coroutine();
                    return;
                }

                response.GateAddress = gateConfig.OuterIPPort.ToString();
                response.GateSessionKey = g2RGetLoginGateKey.GateSessionKey;

                reply();
                session?.Disconnect().Coroutine();
            }
        }
    }
}