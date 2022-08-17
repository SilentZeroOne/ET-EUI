using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(DlgMain))]
    [FriendClassAttribute(typeof(ET.ESItemSlot))]
    [FriendClassAttribute(typeof(ET.Item))]
    public static class DlgMainSystem
    {

        public static void RegisterUIEvent(this DlgMain self)
        {
            self.View.E_InventoryButton.AddListener(self.OnInventoryButtonClick);
        }

        public static void ShowWindow(this DlgMain self, Entity contextData = null)
        {
            self.InitSlots();
        }

        public static void OnInventoryButtonClick(this DlgMain self)
        {
            var uiComponent = self.ZoneScene().GetComponent<UIComponent>();
            bool visiable = uiComponent.IsWindowVisible(WindowID.WindowID_Inventory);
            if (visiable)
            {
                uiComponent.HideWindow(WindowID.WindowID_Inventory);
            }
            else
            {
                uiComponent.ShowWindow(WindowID.WindowID_Inventory);
            }
        }

        public static void OnSlotClick(this DlgMain self, BaseEventData evt, ESItemSlot slot, Item item)
        {
            var isSelected = slot.E_HightLightImage.gameObject.activeInHierarchy;
            slot.E_HightLightImage.gameObject.SetActive(!isSelected);
            self.CurrentItemConfigId = isSelected ? 0 : item.ConfigId;
            for (int i = 0; i < self.Slots.Count; i++)
            {
                if (i != slot.DataId)
                {
                    self.Slots[i].E_HightLightImage.SetVisible(false);
                }
            }

            Game.EventSystem.Publish(new EventType.OnItemSelected() { ZoneScene = self.ZoneScene(), Item = item ,Carried = !isSelected});
        }

        public static void InitSlots(this DlgMain self)
        {
            self.Slots.Add(self.View.ESItemSlot);
            self.Slots.Add(self.View.ESItemSlot1);
            self.Slots.Add(self.View.ESItemSlot2);
            self.Slots.Add(self.View.ESItemSlot3);
            self.Slots.Add(self.View.ESItemSlot4);
            self.Slots.Add(self.View.ESItemSlot5);
            self.Slots.Add(self.View.ESItemSlot6);
            self.Slots.Add(self.View.ESItemSlot7);
            self.Slots.Add(self.View.ESItemSlot8);
            self.Slots.Add(self.View.ESItemSlot9);

            for (int i = 0; i < self.Slots.Count; i++)
            {
                self.Slots[i].uiTransform.gameObject.GetOrAddComponent<MonoBridge>().BelongToEntityId = self.Slots[i].InstanceId;
                self.Slots[i].DataId = i;
            }
            
            self.Refresh();
        }

        public static void Refresh(this DlgMain self)
        {
            InventoryComponent inventoryComponent = self.ZoneScene().GetComponent<InventoryComponent>();

            for (int i = 0; i < self.Slots.Count; i++)
            {
                ESItemSlot slot = self.Slots[i];
                Item item = inventoryComponent.GetItemByIndex(i);
                slot.Init(item, inventoryComponent);
                if (item != null && !item.IsDisposed)
                {
                    slot.E_ItemEventTrigger.RegisterEvent(EventTriggerType.PointerClick, (evt) => self.OnSlotClick(evt, slot, item));
                }
                else
                {
                    slot.E_ItemEventTrigger.triggers.Clear();
                }
            }
        }
    }
}
