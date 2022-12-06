namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgLandCreateRole :Entity,IAwake,IUILogic
	{

		public DlgLandCreateRoleViewComponent View { get => this.Parent.GetComponent<DlgLandCreateRoleViewComponent>();}

		public bool IsCreating;
	}
}
