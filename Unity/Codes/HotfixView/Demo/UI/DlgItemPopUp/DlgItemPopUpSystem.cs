using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgItemPopUp))]
	[FriendClass(typeof(EquipInfoComponent))]
	[FriendClass(typeof(AttributeEntry))]
	public static  class DlgItemPopUpSystem
	{

		public static void RegisterUIEvent(this DlgItemPopUp self)
		{
			self.RegisterCloseEvent<DlgItemPopUp>(self.View.E_CloseButton);
			
			self.View.E_EntrysLoopVerticalScrollRect.AddItemRefreshListener(self.OnEntryLoopHandler);
			self.View.E_EquipButton.AddListenerAsync(self.OnEquipItemHandler);
			self.View.E_UnEquipButton.AddListenerAsync(self.OnUnEquipItemHandler);
			self.View.E_SellButton.AddListenerAsync(self.OnSellItemHandler);
		}

		public static void ShowWindow(this DlgItemPopUp self, Entity contextData = null)
		{
		}
		
		public static void HideWindow(this DlgItemPopUp self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemEntries);
		}

		public static void OnEntryLoopHandler(this DlgItemPopUp self, Transform transform, int index)
		{
			Scroll_Item_itemEntry itemEntry = self.ScrollItemEntries[index].BindTrans(transform);
			Item item = ItemHelper.GetItem(self.ZoneScene(), self.ItemId, self.ItemContainerType);
			AttributeEntry entry = item.GetComponent<EquipInfoComponent>().EntryList[index];
			itemEntry.E_EntryNameText.text = PlayerNumericConfigCategory.Instance.Get(entry.Key).Name + ":";
			bool isPercent = PlayerNumericConfigCategory.Instance.Get(entry.Key).isPercent > 0;
			itemEntry.E_EntryValueText.text = isPercent? $"+{(entry.Value / (float)10000).ToString("0.00")}%" : "+" + entry.Value;
		}

		public static void RefreshInfo(this DlgItemPopUp self, Item item, ItemContainerType itemContainerType)
		{
			self.ItemId = item.Id;
			self.ItemContainerType = itemContainerType;

			self.View.E_IconImage.overrideSprite = IconHelper.LoadIconSprite("Icons", item.Config.Icon);
			self.View.E_QualityImage.color = item.ItemQulityColor();
			self.View.E_NameText.text = item.Config.Name;
			self.View.E_DescText.text = item.Config.Desc;
			self.View.E_SellPriceText.text = item.Config.SellBasePrice.ToString();

			if (item.Config.Type == (int)ItemType.Prop)
			{
				self.View.E_EquipButton.SetVisible(false);
				self.View.E_UnEquipButton.SetVisible(false);
				self.View.E_EntrysLoopVerticalScrollRect.SetVisible(true, 0);
				self.View.E_SellButton.SetVisible(itemContainerType == ItemContainerType.Bag);
			}
			else
			{
				EquipInfoComponent equipInfoComponent = item.GetComponent<EquipInfoComponent>();
				self.View.E_ScoreText.text = equipInfoComponent.Score.ToString();
				self.AddUIScrollItems(ref self.ScrollItemEntries, equipInfoComponent.EntryList.Count);
				self.View.E_EntrysLoopVerticalScrollRect.SetVisible(true, equipInfoComponent.EntryList.Count);
				
				self.View.E_EquipButton.SetVisible(itemContainerType == ItemContainerType.Bag);
				self.View.E_UnEquipButton.SetVisible(itemContainerType == ItemContainerType.RoleInfo);
				self.View.E_SellButton.SetVisible(itemContainerType == ItemContainerType.Bag);
			}
		}

		public static async ETTask OnEquipItemHandler(this DlgItemPopUp self)
		{
			try
			{
				int errorCode = await ItemApplyHelper.EquipItem(self.ZoneScene(), self.ItemId);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Debug($"Equip item: {self.ItemId} error {errorCode}");
					return;
				}
				
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ItemPopUp);
				self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgBag>().Refresh();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
		
		public static async ETTask OnUnEquipItemHandler(this DlgItemPopUp self)
		{
			try
			{
				int errorCode = await ItemApplyHelper.UnEquipItem(self.ZoneScene(), self.ItemId);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ItemPopUp);
				self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgRoleInfo>().Refresh();
				self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgRoleInfo>().RefreshEquipShowItem();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

		public static async ETTask OnSellItemHandler(this DlgItemPopUp self)
		{
			try
			{
				int errorCode = await ItemApplyHelper.SellBagItem(self.ZoneScene(), self.ItemId);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ItemPopUp);
				self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgBag>().Refresh();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}



	}
}
