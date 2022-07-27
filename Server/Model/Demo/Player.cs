namespace ET
{
	public enum PlayerState
	{
		Disconnect,
		Gate,
		Game
	}

	/// <summary>
	/// 玩家角色在Gate网关上的映射
	/// </summary>
	public sealed class Player: Entity, IAwake<string>, IAwake<long, long>,IDestroy
	{
		public long Account { get; set; }

		public Session ClientSession { get; set; }

		public long UnitId { get; set; }

		public PlayerState PlayerState { get; set; }
		
		public long ChatInfoUnitInstanceId { get; set; }
	}
}