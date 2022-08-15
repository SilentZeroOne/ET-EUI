using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(DlgInventory))]
    [FriendClass(typeof(InventoryComponent))]
    [FriendClass(typeof(Item))]
    [FriendClassAttribute(typeof(ET.Scroll_Item_InventorySlot))]
    public static class DlgInventorySystem
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
            self.AddUIScrollItems(ref self.ScrollItemInventorySlots, UnitHelper.GetInventoryCapacityFormZoneScene(self.ZoneScene()));
            self.View.E_SlotsLoopVerticalScrollRect.SetVisible(true, UnitHelper.GetInventoryCapacityFormZoneScene(self.ZoneScene()));
            self.RefreshCoin();
        }

        public static void RefreshSlots(this DlgInventory self)
        {
            self.View.E_SlotsLoopVerticalScrollRect.RefreshCells();
        }

        public static void RefreshCoin(this DlgInventory self)
        {
            self.View.E_CoinCountTextMeshProUGUI.text =
                    UnitHelper.GetMuUnitNumericComponentFromZoneScene(self.ZoneScene()).GetAsInt(NumericType.CoinCount).ToString();
        }

        /// <summary>
        /// ScrollItem里不能放prefab 否则显示会出错
        /// </summary>
        /// <param name="self"></param>
        /// <param name="transform"></param>
        /// <param name="index"></param>
        public static void OnInventoryItemSlotRefresh(this DlgInventory self, Transform transform, int index)
        {
            Scroll_Item_InventorySlot slot = self.ScrollItemInventorySlots[index].BindTrans(transform);
            slot.uiTransform.gameObject.GetOrAddComponent<MonoBridge>().BelongToEntityId = slot.InstanceId;
            
            slot.DataId = index;
            InventoryComponent inventoryComponent = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene()).GetComponent<InventoryComponent>();

            Item item = inventoryComponent.GetItemByIndex(index);
            
            slot.E_ItemEventTrigger.triggers.Clear();
            if (item != null && !item.IsDisposed)
            {
                slot.E_ItemImage.sprite = IconHelper.LoadIconSprite(item.Config.ItemIcon);
                var configId = inventoryComponent.IndexConfigIdDict.GetValueByKey(index);
                
                slot.E_CountTextMeshProUGUI.text = inventoryComponent.GetItemCountByConfigId(configId).ToString();

                slot.E_ItemImage.SetVisible(true);
                slot.E_CountTextMeshProUGUI.SetVisible(true);
                slot.E_HightLightImage.gameObject.SetActive(item.ConfigId == self.CurrentItemConfigId);
                slot.E_ItemEventTrigger.RegisterEvent(EventTriggerType.PointerClick, (data) => self.OnSlotClick(data, slot, item));
                slot.RegisterEvent(item);
            }
            else
            {
                slot.E_ItemImage.SetVisible(false);
                slot.E_CountTextMeshProUGUI.SetVisible(false);
                slot.E_HightLightImage.gameObject.SetActive(false);
            }
        }

        public static void OnSlotClick(this DlgInventory self, BaseEventData eventData, Scroll_Item_InventorySlot itemSlot, Item item)
        {
            var isSelected = itemSlot.E_HightLightImage.gameObject.activeInHierarchy;
            itemSlot.E_HightLightImage.gameObject.SetActive(!isSelected);
            self.CurrentItemConfigId = isSelected ? 0 : item.ConfigId;
            self.View.E_SlotsLoopVerticalScrollRect.RefreshCells();
        }
    }
}
