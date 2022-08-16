using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
    [FriendClass(typeof(ESItemSlot))]
    [FriendClassAttribute(typeof(ET.Item))]
    public static class ESItemSlotSystem
    {
        public static void Init(this ESItemSlot self, Item item,InventoryComponent inventoryComponent)
        {
            self.E_ItemEventTrigger.triggers.Clear();
            if (item != null && !item.IsDisposed)
            {
                self.E_ItemImage.sprite = IconHelper.LoadIconSprite(item.Config.ItemIcon);
                self.E_CountTextMeshProUGUI.text = inventoryComponent.GetItemCountByConfigId(item.ConfigId).ToString();
                
                self.E_ItemImage.SetVisible(true);
                self.E_CountTextMeshProUGUI.SetVisible(true);
                self.E_HightLightImage.gameObject.SetActive(false);
                self.RegisterEvent(item);
            }
            else
            {
                self.E_ItemImage.SetVisible(false);
                self.E_CountTextMeshProUGUI.SetVisible(false);
                self.E_HightLightImage.gameObject.SetActive(false);
            }
        }
        
        public static void RegisterEvent(this ESItemSlot self,Item item)
        {
            self.E_ItemEventTrigger.RegisterEvent(EventTriggerType.BeginDrag, (data) => self.OnSlotBeginDrag(data, item));
            self.E_ItemEventTrigger.RegisterEvent(EventTriggerType.Drag, (data) => self.OnSlotDrag((PointerEventData)data, item));
            self.E_ItemEventTrigger.RegisterEvent(EventTriggerType.EndDrag, (data) => self.OnSlotEndDrag((PointerEventData)data, item).Coroutine());
            self.E_ItemEventTrigger.RegisterEvent(EventTriggerType.PointerEnter, (data) => self.OnSlotPointerEnter((PointerEventData)data, item));
            self.E_ItemEventTrigger.RegisterEvent(EventTriggerType.PointerExit, (data) => self.OnSlotPointerExit((PointerEventData)data, item));
        }

        public static void OnSlotBeginDrag(this ESItemSlot self,BaseEventData eventData,Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_DragItem, new ShowWindowData() { contextData = item });
        }

        public static void OnSlotDrag(this ESItemSlot self, PointerEventData eventData,Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgDragItem>().SyncPosition(eventData.position);
        }

        public static async ETTask OnSlotEndDrag(this ESItemSlot self, PointerEventData eventData, Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_DragItem);
            //如果是Inventory 拖入Inventory 否则生成在地面上
            var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.CompareTag(TagManager.InventoryItemSlot))
                {
                    long id = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<MonoBridge>().BelongToEntityId;
                    Scroll_Item_InventorySlot slot = Game.EventSystem.Get(id) as Scroll_Item_InventorySlot;
                    
                    InventoryComponent actionBarInventoryComponent = self.ZoneScene().GetComponent<InventoryComponent>();
                    actionBarInventoryComponent.RemoveItem(item, false);
                    actionBarInventoryComponent.SaveInventory();
                    
                    InventoryComponent inventoryComponent = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene()).GetComponent<InventoryComponent>();
                    inventoryComponent.AddItemByIndex(item, (int)slot.DataId);
                    inventoryComponent.SaveInventory();

                    await TimerComponent.Instance.WaitAsync(100);
                    
                    self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgInventory>().RefreshSlots();
                    self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgMain>().Refresh();
                }

                if (eventData.pointerCurrentRaycast.gameObject.CompareTag(TagManager.ItemSlot))
                {
                    long id = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<MonoBridge>().BelongToEntityId;
                    ESItemSlot slot = Game.EventSystem.Get(id) as ESItemSlot;
                    
                    InventoryComponent actionBarInventoryComponent = self.ZoneScene().GetComponent<InventoryComponent>();
                    actionBarInventoryComponent.AddItemByIndex(item, (int)slot.DataId);
                    actionBarInventoryComponent.SaveInventory();
                    
                    await TimerComponent.Instance.WaitAsync(100);
                    
                    self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgMain>().Refresh();
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
                    InventoryComponent inventoryComponent = self.ZoneScene().GetComponent<InventoryComponent>();
                    inventoryComponent.RemoveItem(item, false);
                    inventoryComponent.SaveInventory();
                    
                    //加入CurrentScene的Items中
                    ItemsComponent itemsComponent = self.ZoneScene().CurrentScene().GetComponent<ItemsComponent>();
                    itemsComponent.AddChild(item);
                    
                    await TimerComponent.Instance.WaitAsync(100);
                    
                    self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgMain>().Refresh();
                }
            }
        }
        
        private static void OnSlotPointerEnter(this ESItemSlot self, PointerEventData eventData, Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ItemTooltip, new ShowWindowData() { contextData = item });
        }
        
        private static void OnSlotPointerExit(this ESItemSlot self, PointerEventData eventData, Item item)
        {
            self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ItemTooltip);
        }
    }
}