using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(DlgMain))]
    [FriendClassAttribute(typeof(ET.ESItemSlot))]
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

        public static void OnSlotBeginDrag(this DlgMain self, ESItemSlot slot)
        {

        }

        public static void InitSlots(this DlgMain self)
        {
            InventoryComponent inventoryComponent = self.ZoneScene().GetComponent<InventoryComponent>();
            
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
                self.Slots[i].uiTransform.gameObject.AddComponent<MonoBridge>().BelongToEntityId = self.Slots[i].InstanceId;
                self.Slots[i].Init(inventoryComponent.GetItemByIndex(i));
                self.Slots[i].DataId = i;
            }
        }
    }
}
