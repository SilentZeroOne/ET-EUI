namespace ET
{
    public class ItemAwakeSystem : AwakeSystem<Item,int>
    {
        public override void Awake(Item self, int configId)
        {
            self.ConfigId = configId;
        }
    }
    
    public class ItemDestorySystem : DestroySystem<Item>
    {
        public override void Destroy(Item self)
        {
            self.ConfigId = 0;
            self.Quality = 0;
        }
    }

    [FriendClass(typeof(Item))]
    public static class ItemSystem
    {
        public static void FromMessage(this Item self, ItemInfo itemInfo)
        {
            self.Quality = itemInfo.ItemQuality;
            self.Id = itemInfo.ItemUid;
            self.ConfigId = itemInfo.ItemConfigId;

            if (itemInfo.EquipInfo != null)
            {
                
            }
        }
    }
}