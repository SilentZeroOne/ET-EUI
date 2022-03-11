using System;

namespace ET
{
    public class C2G_LoginGameGateHandler: AMRpcHandler<C2G_LoginGameGate,G2C_LoginGameGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Gate)
            {
                Log.Error($"请求的Scene错误，当前Scene为{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }
            
            session.RemoveComponent<SessionAcceptTimeoutComponent>();//需要和Gate网关长时间连接

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RepeatedRequestError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            Scene gateScene = session.DomainScene();
            var key = gateScene.GetComponent<GateSessionKeyComponent>().Get(request.AccountId);
            if (key == null || key != request.Key)
            {
                response.Error = ErrorCode.ERR_ConnectGateKeyError;
                response.Message = "Gate key comfirm error";
                reply();
                session?.Disconnect().Coroutine();
                return;
            }
            
            gateScene.GetComponent<GateSessionKeyComponent>().Remove(request.AccountId);
            long instanceId = session.InstanceId;
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate,request.AccountId.GetHashCode()))
                {
                    if (instanceId != session.InstanceId)
                    {
                        return;
                    }

                    //通知登陆中心服 记录本次登陆的zone(server id)
                    StartSceneConfig loginCenter = StartSceneConfigCategory.Instance.LoginCenterConfig;
                    L2G_AddLoginRecord l2GAddLoginRecord = (L2G_AddLoginRecord) await MessageHelper.CallActor(loginCenter.InstanceId,
                        new G2L_AddLoginRecord() { AccountId = request.AccountId, ServerId = gateScene.Zone });

                    if (l2GAddLoginRecord.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = l2GAddLoginRecord.Error;
                        reply();
                        session?.Disconnect().Coroutine();
                        return;
                    }
                    
                    Player player = gateScene.GetComponent<PlayerComponent>().Get(request.RoleId);
                    if (player == null)
                    {
                        player = gateScene.GetComponent<PlayerComponent>()
                                .AddChildWithId<Player, long, long>(request.RoleId, request.AccountId, request.RoleId);
                        player.PlayerState = PlayerState.Gate;
                        gateScene.GetComponent<PlayerComponent>().Add(player);
                        session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
                    }
                    else
                    {
                        player.RemoveComponent<PlayerOfflineOutTimeComponent>();
                    }

                    session.AddComponent<SessionPlayerComponent>().PlayerId = player.Id;
                    session.GetComponent<SessionPlayerComponent>().PlayerInstanceId = player.InstanceId;
                    player.SessionInstanceId = session.InstanceId;
                }
            }

            reply();
        }
    }
}