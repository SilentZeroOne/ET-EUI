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
            self.E_ItemEventTrigger.RegisterEvent(EventTriggerType.Drag, (data) => self.OnSlotDrag(data, item));
            self.E_ItemEventTrigger.RegisterEvent(EventTriggerType.EndDrag, (data) => self.OnSlotEndDrag(data, item));
        }

        public static void OnSlotBeginDrag(this Scroll_Item_InventorySlot self,BaseEventData eventData,Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_DragItem, new ShowWindowData() { contextData = item });
        }

        public static void OnSlotDrag(this Scroll_Item_InventorySlot self, BaseEventData eventData,Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgDragItem>().SyncPosition(eventData.currentInputModule.input.mousePosition);
        }

        public static void OnSlotEndDrag(this Scroll_Item_InventorySlot self, BaseEventData eventData,Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_DragItem);
        }
        
    }
}