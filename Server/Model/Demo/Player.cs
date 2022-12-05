namespace ET
{
	public enum PlayerState
	{
		Disconnect,
		Gate,
		Game,
		Lobby
	}

	public sealed class Player : Entity, IAwake<long>
	{
		//目前Player.id 和AccountId相同
		public long AccountId { get; set; }

		public PlayerState PlayerState { get; set; }

		public Session ClientSession { get; set; }
	}
}