using System;

namespace ET
{
    public static class ItemApplyHelper
    {
        public static async ETTask<int> EquipItem(Scene zoneScene, long itemId)
        {
            Item item = ItemHelper.GetItem(zoneScene, itemId, ItemContainerType.Bag);

            if (item == null)
            {
                return ErrorCode.ERR_ItemNotExist;
            }

            M2C_EquipItem m2CEquipItem;
            
            try
            {
                m2CEquipItem = (M2C_EquipItem)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_EquipItem() { ItemId = itemId });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            return m2CEquipItem.Error;
        }

        public static async ETTask<int> UnEquipItem(Scene zoneScene, long itemId)
        {
            Item item = ItemHelper.GetItem(zoneScene, itemId, ItemContainerType.RoleInfo);
            if (item == null)
            {
                return ErrorCode.ERR_ItemNotExist;
            }

            M2C_UnloadEquipItem m2CUnloadEquipItem;
            try
            {
                m2CUnloadEquipItem = (M2C_UnloadEquipItem)await zoneScene.GetComponent<SessionComponent>().Session
                        .Call(new C2M_UnloadEquipItem() { EquipPosition = item.Config.EquipPosition });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            return m2CUnloadEquipItem.Error;
        }
        
        public static async ETTask<int> SellBagItem(Scene zoneScene,long itemId)
        {
            Item item = ItemHelper.GetItem(zoneScene, itemId, ItemContainerType.Bag);

            if (item == null)
            {
                return ErrorCode.ERR_ItemNotExist;
            }

            M2C_SellItem m2CSellItem;

            try
            {
                m2CSellItem = (M2C_SellItem)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_SellItem() { ItemUid = itemId });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            return m2CSellItem.Error;
        }
    }
}