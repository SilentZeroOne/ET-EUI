using System.Collections.Generic;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgInventory :Entity,IAwake,IUILogic
	{

		public DlgInventoryViewComponent View { get => this.Parent.GetComponent<DlgInventoryViewComponent>();}

		public Dictionary<int, Scroll_Item_InventorySlot> ScrollItemInventorySlots = new Dictionary<int, Scroll_Item_InventorySlot>();

		public int CurrentItemConfigId;
	}
}
