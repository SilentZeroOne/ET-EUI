using UnityEngine;

namespace ET
{
    [FriendClass(typeof(ES_EquipItem))]
    public static class ESEquipItemSystem
    {
        public static void RefreshShowItem(this ES_EquipItem self, EquipPosition equipPosition)
        {
            Item item = self.ZoneScene().GetComponent<EquipmentsComponent>().GetEquipItemByPosition(equipPosition);
            if (item != null)
            {
                self.E_IconImage.overrideSprite = IconHelper.LoadIconSprite("Icons", item.Config.Icon);
                self.E_QualityImage.color = item.ItemQulityColor();
            }
            else
            {
                self.E_IconImage.sprite = null;
                self.E_IconImage.overrideSprite = null;
                self.E_QualityImage.color = Color.white;
            }
        }

        public static void RegisterEventHandler(this ES_EquipItem self, EquipPosition equipPosition)
        {
            self.E_SelectButton.AddListenerWithParam(self.OnSelectItemHandler, equipPosition);
        }
        
        public static void OnSelectItemHandler(this ES_EquipItem self,EquipPosition equipPosition)
        {
            Item item = self.ZoneScene().GetComponent<EquipmentsComponent>().GetEquipItemByPosition(equipPosition);
            if (item == null)
            {
                return;
            }
            
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ItemPopUp);
            self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgItemPopUp>().RefreshInfo(item, ItemContainerType.RoleInfo);
        }
    }
}