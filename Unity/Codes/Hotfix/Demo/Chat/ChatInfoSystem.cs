namespace ET
{
    public class ChatInfoAwakeSystem: AwakeSystem<ChatInfo>
    {
        public override void Awake(ChatInfo self)
        {

        }
    }

    public class ChatInfoDestroySystem: DestroySystem<ChatInfo>
    {
        public override void Destroy(ChatInfo self)
        {

        }
    }

    [FriendClass(typeof (ChatInfo))]
    public static class ChatInfoSystem
    {
        public static void Test(this ChatInfo self)
        {
        }

        
    }
}