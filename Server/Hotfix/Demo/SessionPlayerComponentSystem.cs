

namespace ET
{
	public static class SessionPlayerComponentSystem
	{
		public class SessionPlayerComponentDestroySystem: DestroySystem<SessionPlayerComponent>
		{
			public override void Destroy(SessionPlayerComponent self)
			{
				// 发送断线消息
				if (self.OtherPlayLogin)
				{
					ActorLocationSenderComponent.Instance.Send(self.PlayerId, new G2M_SessionDisconnect());
					Log.Debug($"玩家 {self.PlayerId} 被踢下线");
				}
				else
				{
					DisconnectHelper.KickPlayer((Player) Game.EventSystem.Get(self.PlayerInstanceId)).Coroutine();
					Log.Debug($"玩家 {self.PlayerId} 自动断线");
				}
			}
		}

		public static Player GetMyPlayer(this SessionPlayerComponent self)
		{
			return self.Domain.GetComponent<PlayerComponent>().Get(self.AccountId);
		}
	}
}
