namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgItemTooltip :Entity,IAwake,IUILogic
	{

		public DlgItemTooltipViewComponent View { get => this.Parent.GetComponent<DlgItemTooltipViewComponent>();} 

		 

	}
}
