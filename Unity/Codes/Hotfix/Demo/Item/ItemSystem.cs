namespace ET
{
    
    public class ItemAwake1System: AwakeSystem<Item>
    {
        
        public override void Awake(Item self)
        {
           
        }
    }
    public class ItemAwakeSystem: AwakeSystem<Item,int>
    {
        
        public override void Awake(Item self, int a)
        {
            self.ConfigId = a;
        }
    }

    public class ItemDestroySystem: DestroySystem<Item>
    {
        public override void Destroy(Item self)
        {

        }
    }

    [FriendClass(typeof (Item))]
    public static class ItemSystem
    {
        public static ItemInfo ToProto(this Item self)
        {
            return new ItemInfo() { ConfigId = self.ConfigId, ItemId = self.Id };
        }

        public static Item FromProto(this Item self, ItemInfo proto)
        {
            self.Id = proto.ItemId;
            self.ConfigId = proto.ConfigId;

            return self;
        }
    }
}