namespace ET
{
    [FriendClass(typeof(Item))]
    [FriendClass(typeof(BagComponent))]
    [FriendClass(typeof(EquipmentsComponent))]
    public static class ItemUpdateNoticeHelper
    {
        public static void SyncAddItem(Unit unit, Item item, M2C_ItemUpdateOpInfo message)
        {
            message.ItemInfo = item.ToMessage();
            message.Op = (int)ItemOp.Add;
            MessageHelper.SendToClient(unit,message);
        }
        
        public static void SyncRemoveItem(Unit unit, Item item, M2C_ItemUpdateOpInfo message)
        {
            message.ItemInfo = item.ToMessage(false);
            message.Op = (int)ItemOp.Remove;
            MessageHelper.SendToClient(unit,message);
        }

        public static void SyncAllBagItems(Unit unit)
        {
            M2C_AllItemList m2CAllItemList = new M2C_AllItemList(){ContainerType = (int)ItemContainerType.Bag};
            BagComponent bagComponent = unit.GetComponent<BagComponent>();
            foreach (var item in bagComponent.ItemsDict.Values)
            {
                m2CAllItemList.ItemInfoList.Add(item.ToMessage());
            }

            MessageHelper.SendToClient(unit, m2CAllItemList);
        }

        public static void SyncAllEquipItems(Unit unit)
        {
            M2C_AllItemList m2CAllItemList = new M2C_AllItemList(){ContainerType = (int)ItemContainerType.RoleInfo};
            EquipmentsComponent equipmentsComponent = unit.GetComponent<EquipmentsComponent>();
            foreach (var equip in equipmentsComponent.EquipItems.Values)
            {
                m2CAllItemList.ItemInfoList.Add(equip.ToMessage());
            }

            MessageHelper.SendToClient(unit, m2CAllItemList);
        }
    }
}