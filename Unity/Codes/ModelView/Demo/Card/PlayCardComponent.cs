namespace ET
{
	[ComponentOf()]
	public class PlayCardComponent: Entity, IAwake, IDestroy
	{
		public EntityMonoBridge MonoBridge { get; set; }
		public bool IsSelected { get; set; }
		
		//通过划拽选中
		public bool PointerSelected { get; set; }
	}
}