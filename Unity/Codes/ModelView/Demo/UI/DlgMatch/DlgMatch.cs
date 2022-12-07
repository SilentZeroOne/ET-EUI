namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgMatch :Entity,IAwake,IUILogic
	{

		public DlgMatchViewComponent View { get => this.Parent.GetComponent<DlgMatchViewComponent>();}

		public bool IsMatching;

	}
}
