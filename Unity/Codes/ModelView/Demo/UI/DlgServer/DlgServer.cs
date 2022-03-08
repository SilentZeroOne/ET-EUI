namespace ET
{
	public  class DlgServer :Entity,IAwake
	{

		public DlgServerViewComponent View { get => this.Parent.GetComponent<DlgServerViewComponent>();} 

		 

	}
}
