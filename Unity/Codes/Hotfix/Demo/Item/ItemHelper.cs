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

        public static void RemoveItem(Scene zoneScene, long itemId, ItemContainerType containerType)
        {
            
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