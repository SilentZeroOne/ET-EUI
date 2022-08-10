
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_InventorySlotDestroySystem : DestroySystem<Scroll_Item_InventorySlot> 
	{
		public override void Destroy( Scroll_Item_InventorySlot self )
		{
			self.DestroyWidget();
		}
	}
}
