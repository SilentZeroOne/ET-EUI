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

        public static void AddItem(this BagComponent self, Item item)
        {
            self.AddChild(item);
            self.ItemsDict.Add(item.Id, item);
            self.ItemMap.Add(item.Config.Type, item);
        }

        public static void RemoveItem(this BagComponent self, Item item)
        {
            if (item == null)
            {
                Log.Error("Item is null");
                return;
            }
            
            self.ItemsDict.Remove(item.Id);
            self.ItemMap.Remove(item.Config.Type, item);
            item?.Dispose();
        }

        public static void Clear(this BagComponent self)
        {
            ForeachHelper.Foreach(self.ItemsDict, (id, item) =>
            {
                item?.Dispose();
            });
            self.ItemsDict.Clear();
            self.ItemMap.Clear();
        }
    }
}