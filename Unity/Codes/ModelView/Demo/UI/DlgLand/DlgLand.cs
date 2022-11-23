namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgLand :Entity,IAwake,IUILogic
	{
		public DlgLandViewComponent View { get => this.Parent.GetComponent<DlgLandViewComponent>();}

		public bool IsLogining { get; set; }

		public bool IsRegistering { get; set; }
	}
}
