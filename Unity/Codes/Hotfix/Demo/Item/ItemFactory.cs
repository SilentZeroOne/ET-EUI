namespace ET
{
    [FriendClass(typeof(Item))]
    public static class ItemFactory
    {
        public static Item Create(Entity self, int configId)
        {
            Item item = self.AddChild<Item, int>(configId);
            return item;
        }
        
        public static Item Create(Entity self, ItemInfo info)
        {
            Item item = self?.AddChild<Item, int>(info.ItemConfigId);
            item?.FromMessage(info);

            return item;
        }
    }
}