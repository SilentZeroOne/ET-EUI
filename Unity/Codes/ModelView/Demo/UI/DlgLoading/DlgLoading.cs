using System.Text;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgLoading :Entity,IAwake,IUILogic
	{

		public DlgLoadingViewComponent View { get => this.Parent.GetComponent<DlgLoadingViewComponent>();}

		public long Timer;

		public StringBuilder LoadingTextBuilder;
	}
}
