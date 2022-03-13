using System;

namespace ET
{
    public static class DisconnectHelper
    {
        public static async ETTask Disconnect(this Session self)
        {
            if(self==null||self.IsDisposed) return;

            var instanceId = self.InstanceId;

            await TimerComponent.Instance.WaitAsync(1000);

            if (self.InstanceId == instanceId)
            {
                self.Dispose();
            }
        }

        public static async ETTask KickPlayer(Player player,bool isException = false)
        {
            if (player == null || player.IsDisposed)
            {
                return;
            }

            long instanceId = player.InstanceId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate,player.Account.GetHashCode()))
            {
                if (player.IsDisposed || instanceId != player.InstanceId)
                {
                    return;
                }

                if (!isException)
                {
                    switch (player.PlayerState)
                    {
                        case PlayerState.Disconnect:
                            break;
                        case PlayerState.Gate:
                            break;
                        case PlayerState.Game:
                            //TODO 通知游戏逻辑服下线玩家，并将数据存入数据库
                            var m2GRequestExitGame =
                                    (M2G_RequestExitGame) await MessageHelper.CallLocationActor(player.UnitId, new G2M_RequestExitGame());

                            long loginCenterConfig = StartSceneConfigCategory.Instance.LoginCenterConfig.InstanceId;
                            var l2gRemoveRecord = (L2G_RemoveLoginRecord) await MessageHelper.CallActor(loginCenterConfig,
                                new G2L_RemoveLoginRecord() { AccountId = player.Account, ServerId = player.DomainZone() });
                            
                            break;
                    }
                }

                player.PlayerState = PlayerState.Disconnect;
                player.DomainScene().GetComponent<PlayerComponent>()?.Remove(player.Account);
                player?.Dispose();
                await TimerComponent.Instance.WaitAsync(300);
            }
        }
    }
}