namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgDragItem :Entity,IAwake,IUILogic
	{

		public DlgDragItemViewComponent View { get => this.Parent.GetComponent<DlgDragItemViewComponent>();} 

		 

	}
}
