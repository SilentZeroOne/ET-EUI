using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.SessionPlayerComponent))]
    public class C2G_LoginGameGateHandler : AMRpcHandler<C2G_LoginGameGate, G2C_LoginGameGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Gate)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{session.DomainScene().SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                session.Dispose();
                return;
            }

            session.RemoveComponent<SessionAcceptTimeoutComponent>();//移除超时组件 保持长链接

            // 防止重复请求
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RepeatedRequestError;
                reply();
                return;
            }

            Scene gateScene = session.DomainScene();
            var sessionKey = gateScene.GetComponent<GateSessionKeyComponent>().Get(request.AccountId);
            if (sessionKey == null || sessionKey != request.GateSessionKey)
            {
                response.Error = ErrorCode.ERR_ConnectGateKeyError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            gateScene.GetComponent<GateSessionKeyComponent>().Remove(request.AccountId);

            long instanceId = session.InstanceId;
            using (session.AddComponent<SessionLockingComponent>())
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, request.AccountId.GetHashCode()))
            {
                if (instanceId != session.InstanceId)
                {
                    response.Error = ErrorCode.ERR_PlayerSessionError;
                    reply();
                    session?.Disconnect().Coroutine();
                    return;
                }

                SessionStateComponent SessionStateComponent = session.GetComponent<SessionStateComponent>();
                if (SessionStateComponent == null)
                {
                    SessionStateComponent = session.AddComponent<SessionStateComponent>();
                }
                SessionStateComponent.State = SessionState.Normal;

                Player player = gateScene.GetComponent<PlayerComponent>().Get(request.AccountId);
                if (player == null)
                {
                    player = gateScene.GetComponent<PlayerComponent>().AddChildWithId<Player, long>(request.RoleInfoId, request.AccountId);
                    player.PlayerState = PlayerState.Gate;
                    gateScene.GetComponent<PlayerComponent>().Add(player);
                    session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
                }
                else
                {
                    //TODO playerofflinetimeoutComponent  断线延迟10秒再正式断开链接
                    //登陆中心服相关
                }

                var sessionPlayer = session.AddComponent<SessionPlayerComponent>();
                sessionPlayer.AccountId = request.AccountId;
                sessionPlayer.PlayerId = player.Id;
                sessionPlayer.PlayerInstanceId = player.InstanceId;
                player.ClientSession = session;
                
                response.PlayerId = player.Id;
            }
            reply();
        }
    }
}