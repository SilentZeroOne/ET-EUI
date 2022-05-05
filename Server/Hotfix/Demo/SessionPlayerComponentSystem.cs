

namespace ET
{
	[FriendClass(typeof(SessionPlayerComponent))]
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
				self.Revert();
			}
		}

		public static Player GetMyPlayer(this SessionPlayerComponent self)
		{
			return self.Domain.GetComponent<PlayerComponent>().Get(self.AccountId);
		}

		public static void Revert(this SessionPlayerComponent self)
		{
			self.AccountId = 0;
			self.PlayerId = 0;
			self.OtherPlayLogin = false;
			self.PlayerInstanceId = 0;
		}
	}
}
