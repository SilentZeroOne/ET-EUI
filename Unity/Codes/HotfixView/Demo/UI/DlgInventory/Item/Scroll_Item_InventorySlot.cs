﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
    [FriendClass(typeof (Scroll_Item_InventorySlot))]
    public static class Scroll_Item_InventorySlotSystem
    {
        public static void RegisterEvent(this Scroll_Item_InventorySlot self,Item item)
        {
            self.E_ItemEventTrigger.RegisterEvent(EventTriggerType.BeginDrag, (data) => self.OnSlotBeginDrag(data, item));
            self.E_ItemEventTrigger.RegisterEvent(EventTriggerType.Drag, (data) => self.OnSlotDrag((PointerEventData)data, item));
            self.E_ItemEventTrigger.RegisterEvent(EventTriggerType.EndDrag, (data) => self.OnSlotEndDrag((PointerEventData)data, item).Coroutine());
        }

        public static void OnSlotBeginDrag(this Scroll_Item_InventorySlot self,BaseEventData eventData,Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_DragItem, new ShowWindowData() { contextData = item });
        }

        public static void OnSlotDrag(this Scroll_Item_InventorySlot self, PointerEventData eventData,Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgDragItem>().SyncPosition(eventData.position);
        }

        public static async ETTask OnSlotEndDrag(this Scroll_Item_InventorySlot self, PointerEventData eventData,Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_DragItem);
            //如果是快捷栏 拖入快捷栏 否则生成在地面上
            var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.CompareTag(TagManager.ItemSlot))
                {
                    long id = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<MonoBridge>().BelongToEntityId;
                    ESItemSlot slot = Game.EventSystem.Get(id) as ESItemSlot;
                    
                    InventoryComponent inventoryComponent = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene()).GetComponent<InventoryComponent>();
                    inventoryComponent.RemoveItem(item, false);
                    inventoryComponent.SaveInventory();
                    
                    InventoryComponent actionBarInventoryComponent = self.ZoneScene().GetComponent<InventoryComponent>();
                    actionBarInventoryComponent.AddItemByIndex(item, (int)slot.DataId);
                    actionBarInventoryComponent.SaveInventory();
                    
                    await TimerComponent.Instance.WaitAsync(100);
                    
                    self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgInventory>().RefreshSlots();
                    self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgMain>().Refresh();
                }

                if (eventData.pointerCurrentRaycast.gameObject.CompareTag(TagManager.InventoryItemSlot))
                {
                    long id = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<MonoBridge>().BelongToEntityId;
                    Scroll_Item_InventorySlot slot = Game.EventSystem.Get(id) as Scroll_Item_InventorySlot;
                    
                    InventoryComponent inventoryComponent = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene()).GetComponent<InventoryComponent>();
                    inventoryComponent.AddItemByIndex(item, (int)slot.DataId);
                    inventoryComponent.SaveInventory();
                    
                    await TimerComponent.Instance.WaitAsync(100);
                    
                    self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgInventory>().RefreshSlots();
                }
            }
            else
            {
                if (item.Config.CanDropped == 1)
                {
                    Game.EventSystem.Publish(new EventType.AfterItemCreate()
                    {
                        Item = item,
                        UsePos = true,
                        X = worldPos.x,
                        Y = worldPos.y,
                    });
                
                    //从Inventory中移除 但不要dispose
                    InventoryComponent inventoryComponent = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene()).GetComponent<InventoryComponent>();
                    inventoryComponent.RemoveItem(item, false);
                    inventoryComponent.SaveInventory();
                    //加入CurrentScene的Items中
                    ItemsComponent itemsComponent = self.ZoneScene().CurrentScene().GetComponent<ItemsComponent>();
                    itemsComponent.AddChild(item);
            
                    await TimerComponent.Instance.WaitAsync(100);
                    
                    self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgInventory>().RefreshSlots();
                }
            }
            
        }
        
    }
}