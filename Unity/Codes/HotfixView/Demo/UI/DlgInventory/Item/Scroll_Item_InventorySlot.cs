using UnityEngine;
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
            self.E_ItemEventTrigger.RegisterEvent(EventTriggerType.EndDrag, (data) => self.OnSlotEndDrag((PointerEventData)data, item));
        }

        public static void OnSlotBeginDrag(this Scroll_Item_InventorySlot self,BaseEventData eventData,Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_DragItem, new ShowWindowData() { contextData = item });
        }

        public static void OnSlotDrag(this Scroll_Item_InventorySlot self, PointerEventData eventData,Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgDragItem>().SyncPosition(eventData.position);
        }

        public static void OnSlotEndDrag(this Scroll_Item_InventorySlot self, PointerEventData eventData,Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_DragItem);
            //如果是陆地 生成Item到陆地上
            var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.CompareTag(TagManager.Land))
                {
                    
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
            
                    self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgInventory>().Refresh();
                }
            }
            
        }
        
    }
}