using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgInventory))]
	[FriendClass(typeof(InventoryComponent))]
	[FriendClass(typeof(Item))]
	public static  class DlgInventorySystem
	{

		public static void RegisterUIEvent(this DlgInventory self)
		{
			self.View.E_SlotsLoopVerticalScrollRect.AddItemRefreshListener(self.OnInventoryItemSlotRefresh);
		}

		public static void ShowWindow(this DlgInventory self, Entity contextData = null)
		{
			self.Refresh();
		}

		public static void HideWindow(this DlgInventory self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemInventorySlots);
			
		}

		public static void Refresh(this DlgInventory self)
		{
			self.AddUIScrollItems(ref self.ScrollItemInventorySlots, Settings.MaxInventorySlot);
			self.View.E_SlotsLoopVerticalScrollRect.SetVisible(true, Settings.MaxInventorySlot);
			self.RefreshCoin();
		}

		public static void RefreshCoin(this DlgInventory self)
		{
			self.View.E_CoinCountTextMeshProUGUI.text = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene()).GetComponent<NumericComponent>()
					.GetAsInt(NumericType.CoinCount).ToString();
		}

		public static void OnInventoryItemSlotRefresh(this DlgInventory self, Transform transform, int index)
		{
			Scroll_Item_InventorySlot slot = self.ScrollItemInventorySlots[index].BindTrans(transform);
			InventoryComponent inventoryComponent = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene()).GetComponent<InventoryComponent>();
			if (index <= inventoryComponent.ItemConfigIdList.Count - 1)
			{
				Item item = inventoryComponent.GetItemByConfigId(inventoryComponent.ItemConfigIdList[index]);
				slot.E_ItemImage.sprite = IconHelper.LoadIconSprite(item.Config.ItemIcon);
				slot.E_CountTextMeshProUGUI.text = inventoryComponent.GetItemCountByConfigId(inventoryComponent.ItemConfigIdList[index]).ToString();
				
				slot.E_ItemImage.SetVisible(true);
				slot.E_CountTextMeshProUGUI.SetVisible(true);
			}
			else
			{
				slot.E_ItemImage.SetVisible(false);
				slot.E_CountTextMeshProUGUI.SetVisible(false);
			}
		}
		 

	}
}
