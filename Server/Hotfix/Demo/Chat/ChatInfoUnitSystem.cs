namespace ET
{
    public class ChatInfoUnitAwakeSystem: AwakeSystem<ChatInfoUnit>
    {
        public override void Awake(ChatInfoUnit self)
        {

        }
    }

    public class ChatInfoUnitDestroySystem: DestroySystem<ChatInfoUnit>
    {
        public override void Destroy(ChatInfoUnit self)
        {

        }
    }

    [FriendClass(typeof (ChatInfoUnit))]
    public static class ChatInfoUnitSystem
    {
        public static void Test(this ChatInfoUnit self)
        {
        }

        
    }
}