using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.SessionPlayerComponent))]
    public class C2G_EnterGameHandler : AMRpcHandler<C2G_EnterGame, G2C_EnterGame>
    {
        protected override async ETTask Run(Session session, C2G_EnterGame request, G2C_EnterGame response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Gate)
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

            SessionPlayerComponent sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
            if (sessionPlayerComponent == null)
            {
                response.Error = ErrorCode.ERR_SessionPlayerError;
                reply();
                return;
            }

            Player player = Game.EventSystem.Get(sessionPlayerComponent.PlayerInstanceId) as Player;
            if (player == null || player.IsDisposed)
            {
                response.Error = ErrorCode.ERR_NonePlayerError;
                reply();
                return;
            }

            long sessionInstanceId = session.InstanceId;
            using (session.AddComponent<SessionLockingComponent>())
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate,player.AccountId.GetHashCode()))
            {
                if (sessionInstanceId != session.InstanceId || player.IsDisposed)
                {
                    response.Error = ErrorCode.ERR_PlayerSessionError;
                    reply();
                    return;
                }

                var state = session.GetComponent<SessionStateComponent>();
                if (state != null && state.State == SessionState.Game)
                {
                    response.Error = ErrorCode.ERR_SessionStateError;
                    reply();
                    return;
                }

                if (player.PlayerState == PlayerState.Game)
                {
                    //TODO 断线重登进入已经存在的游戏
                }

                try
                {
                    
                }
                catch (Exception e)
                {
                    Log.Error($"角色进入游戏逻辑服出现问题 账号Id: {player.AccountId}  角色Id: {player.Id}   异常信息： {e.ToString()}");
                    response.Error = ErrorCode.ERR_EnterGameError;
                    reply();
                    await DisconnectHelper.KickPlayer(player, true);
                    session.Disconnect().Coroutine();
                }
                
            }


            await ETTask.CompletedTask;
        }
    }
}