namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgLandLobby :Entity,IAwake,IUILogic
	{

		public DlgLandLobbyViewComponent View { get => this.Parent.GetComponent<DlgLandLobbyViewComponent>();}

		public bool IsEntering;

	}
}
