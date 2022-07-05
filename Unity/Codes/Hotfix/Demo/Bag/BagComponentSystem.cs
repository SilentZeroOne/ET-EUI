namespace ET
{
    [FriendClass(typeof(BagComponent))]
    public static class BagComponentSystem
    {
        public static int GetItemCountByItemType(this BagComponent self, ItemType itemType)
        {
            if (self.ItemMap.TryGetValue((int)itemType, out var itemList))
            {
                return itemList.Count;
            }

            return 0;
        }

        public static Item GetItemById(this BagComponent self, long id)
        {
            if (self.ItemsDict.TryGetValue(id, out var item))
            {
                return item;
            }

            return null;
        }
    }
}