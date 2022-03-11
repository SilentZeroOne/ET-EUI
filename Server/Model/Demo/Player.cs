namespace ET
{
	public enum PlayerState
	{
		Disconnect,
		Gate,
		Game
	}
	
	[ObjectSystem]
	public class PlayerSystem : AwakeSystem<Player, long,long>
	{
		public override void Awake(Player self, long accountId, long roleId)
		{
			self.Account = accountId;
			self.UnitId = roleId;
		}
	}

	/// <summary>
	/// 玩家角色在Gate网关上的映射
	/// </summary>
	public sealed class Player : Entity, IAwake<string>,IAwake<long,long>
	{
		public long Account { get; set; }
		
		public long SessionInstanceId { get; set; }
		
		public long UnitId { get; set; }

		public PlayerState PlayerState { get; set; }
	}
}