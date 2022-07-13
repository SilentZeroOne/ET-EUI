namespace ET
{
    public static class ItemHelper
    {
        public static void AddItem(Scene zoneScene, Item item, ItemContainerType containerType)
        {
            if (containerType == ItemContainerType.Bag)
            {
                zoneScene.GetComponent<BagComponent>().AddItem(item);
            }
            else if (containerType == ItemContainerType.RoleInfo)
            {
                zoneScene.GetComponent<EquipmentsComponent>().EquipItem(item);
            }
        }

        public static void RemoveItemById(Scene zoneScene, long itemId, ItemContainerType containerType)
        {
            Item item = GetItem(zoneScene, itemId, containerType);
            if (containerType == ItemContainerType.Bag)
            {
                zoneScene.GetComponent<BagComponent>().RemoveItem(item);
            }
            else if (containerType == ItemContainerType.RoleInfo)
            {
                zoneScene.GetComponent<EquipmentsComponent>().UnEquipItem(item);
            }
        }

        public static Item GetItem(Scene zoneScene, long itemId, ItemContainerType containerType)
        {
            if (containerType == ItemContainerType.Bag)
            {
                return zoneScene.GetComponent<BagComponent>().GetItemById(itemId);
            }

            if (containerType == ItemContainerType.RoleInfo)
            {
                return zoneScene.GetComponent<EquipmentsComponent>().GetEquipItemByID(itemId);
            }

            return null;
        }

        public static void Clear(Scene zoneScene, ItemContainerType containerType)
        {
            if (containerType == ItemContainerType.Bag)
            {
                zoneScene?.GetComponent<BagComponent>().Clear();
            }
            else if (containerType == ItemContainerType.RoleInfo)
            {
                zoneScene?.GetComponent<EquipmentsComponent>().Clear();
            }
        }
    }
}