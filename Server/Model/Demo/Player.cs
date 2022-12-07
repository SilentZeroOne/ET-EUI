namespace ET
{
	public enum PlayerState
	{
		Disconnect,
		Gate,
		Game,
		Lobby
	}

	public sealed class Player : Entity, IAwake<long>,IDestroy
	{
		//Player.id 和RoleInfoId相同
		public long AccountId { get; set; }

		public PlayerState PlayerState { get; set; }

		public Session ClientSession { get; set; }
	}
}