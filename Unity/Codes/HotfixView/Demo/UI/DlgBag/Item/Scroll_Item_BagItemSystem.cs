namespace ET
{
    public static class Scroll_Item_BagItemSystem
    {
        public static void Refresh(this Scroll_Item_BagItem self,long id)
        {
            Item item = self.ZoneScene().GetComponent<BagComponent>().GetItemById(id);
            self.E_QualityImage.color = item.ItemQulityColor();
            self.E_SelectButton.AddListenerWithId(self.OnShowItemEntryPopUpHandler, id);
        }

        public static void OnShowItemEntryPopUpHandler(this Scroll_Item_BagItem self,long id)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ItemPopUp);
            Item item = self.ZoneScene().GetComponent<BagComponent>().GetItemById(id);
            self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgItemPopUp>().RefreshInfo(item, ItemContainerType.Bag);
        }
    }
}