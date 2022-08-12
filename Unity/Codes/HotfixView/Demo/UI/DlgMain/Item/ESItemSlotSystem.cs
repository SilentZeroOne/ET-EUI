namespace ET
{
    [FriendClass(typeof(ESItemSlot))]
    [FriendClassAttribute(typeof(ET.Item))]
    public static class ESItemSlotSystem
    {
        public static void Init(this ESItemSlot self, Item item)
        {
            InventoryComponent inventoryComponent = self.ZoneScene().GetComponent<InventoryComponent>();
            if (item != null && !item.IsDisposed)
            {
                self.E_ItemImage.sprite = IconHelper.LoadIconSprite(item.Config.ItemIcon);
                self.E_CountTextMeshProUGUI.text = inventoryComponent.GetItemCountByConfigId(item.ConfigId).ToString();
                
                self.E_ItemImage.SetVisible(true);
                self.E_CountTextMeshProUGUI.SetVisible(true);
                self.E_HightLightImage.gameObject.SetActive(false);
            }
            else
            {
                self.E_ItemImage.SetVisible(false);
                self.E_CountTextMeshProUGUI.SetVisible(false);
                self.E_HightLightImage.gameObject.SetActive(false);
                self.E_ItemEventTrigger.triggers.Clear();
            }
        }
    }
}