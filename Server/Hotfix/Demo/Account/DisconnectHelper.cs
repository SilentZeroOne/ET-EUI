namespace ET
{
    public static class DisconnectHelper
    {
        public static async ETTask Disconnect(this Session self)
        {
            if (self == null || self.IsDisposed) return;

            var instanceId = self.InstanceId;

            await TimerComponent.Instance.WaitAsync(1000);

            if (self.InstanceId == instanceId)
            {
                self.Dispose();
            }
        }

        public static async ETTask KickPlayer(Player player,bool isExpection = false)
        {
            if (player == null || player.IsDisposed)
            {
                return;
            }

            var instanceId = player.InstanceId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate,player.AccountId.GetHashCode()))
            {
                if (player.IsDisposed || player.InstanceId != instanceId)
                {
                    return;
                }

                if (!isExpection)
                {
                    switch (player.PlayerState)
                    {
                        case PlayerState.Disconnect:
                            break;
                        case PlayerState.Gate:
                            break;
                        case PlayerState.Game:
                            //WORKFLOW 玩家下线操作 都需要在这里增加
                            //通知游戏逻辑服下线玩家，并将数据存入数据库
                            await MessageHelper.CallLocationActor(player.AccountId, new G2M_RequestExitGame());
                            break;
                    }
                }

                player.PlayerState = PlayerState.Disconnect;
                player.DomainScene().GetComponent<PlayerComponent>()?.Remove(player.AccountId);
                player?.Dispose();
                await TimerComponent.Instance.WaitAsync(300);
            }
        }
    }
}