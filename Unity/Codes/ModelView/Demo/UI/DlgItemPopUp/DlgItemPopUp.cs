using System.Collections.Generic;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgItemPopUp :Entity,IAwake,IUILogic
	{

		public DlgItemPopUpViewComponent View { get => this.Parent.GetComponent<DlgItemPopUpViewComponent>();}

		public Dictionary<int, Scroll_Item_itemEntry> ScrollItemEntries = new Dictionary<int, Scroll_Item_itemEntry>();

		public long ItemId = 0;

		public ItemContainerType ItemContainerType = ItemContainerType.Bag;

	}
}
