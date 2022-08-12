using System.Collections.Generic;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgMain :Entity,IAwake,IUILogic
	{

		public DlgMainViewComponent View { get => this.Parent.GetComponent<DlgMainViewComponent>();}

		public List<ESItemSlot> Slots = new List<ESItemSlot>(10);

	}
}
