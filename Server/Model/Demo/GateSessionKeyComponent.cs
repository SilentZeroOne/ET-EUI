using System.Collections.Generic;

namespace ET
{
	public class GateSessionKeyComponent : Entity, IAwake
	{
		private readonly Dictionary<long, string> sessionKey = new Dictionary<long, string>();
		
		public void Add(long key, string token)
		{
			this.sessionKey.Add(key, token);
			this.TimeoutRemoveKey(key).Coroutine();
		}

		public string Get(long key)
		{
			string account = null;
			this.sessionKey.TryGetValue(key, out account);
			return account;
		}

		public void Remove(long key)
		{
			this.sessionKey.Remove(key);
		}

		private async ETTask TimeoutRemoveKey(long key)
		{
			await TimerComponent.Instance.WaitAsync(20000);
			this.sessionKey.Remove(key);
		}
	}
}
