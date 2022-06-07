namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	public  class DlgMain :Entity,IAwake
	{

		public DlgMainViewComponent View { get => this.Parent.GetComponent<DlgMainViewComponent>();} 

		 

	}
}
