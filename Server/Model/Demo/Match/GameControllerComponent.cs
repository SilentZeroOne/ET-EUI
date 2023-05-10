namespace ET
{
	[ComponentOf()]
	public class GameControllerComponent: Entity, IAwake, IDestroy
	{
		//倍率
		public int Multiples { get; set; } = 1;
	}
}