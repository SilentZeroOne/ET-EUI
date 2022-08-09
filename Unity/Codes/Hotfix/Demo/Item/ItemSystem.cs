namespace ET
{
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
        public static void Test(this Item self)
        {
        }
        
    }
}