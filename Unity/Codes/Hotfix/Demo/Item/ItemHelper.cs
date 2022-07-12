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
                
            }
        }

        public static Item GetItem(Scene zoneScene, long itemId, ItemContainerType containerType)
        {
            if (containerType == ItemContainerType.Bag)
            {
                return zoneScene.GetComponent<BagComponent>().GetItemById(itemId);
            }
            else if (containerType == ItemContainerType.RoleInfo)
            {
                
            }

            return null;
        }

        public static void Clear(Scene zoneScene, ItemContainerType containerType)
        {
            if (containerType == ItemContainerType.Bag)
            {
                zoneScene?.GetComponent<BagComponent>().Clear();
            }
        }
    }
}