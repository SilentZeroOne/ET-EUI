namespace ET
{
    public class ChatComponentAwakeSystem: AwakeSystem<ChatComponent>
    {
        public override void Awake(ChatComponent self)
        {

        }
    }

    public class ChatComponentDestroySystem: DestroySystem<ChatComponent>
    {
        public override void Destroy(ChatComponent self)
        {
            foreach (var info in self.ChatMessageQueue)
            {
                info?.Dispose();
            }
            self.ChatMessageQueue.Clear();
        }
    }
    

    [FriendClass(typeof (ChatComponent))]
    public static class ChatComponentSystem
    {
        public static int GetChatMessageCount(this ChatComponent self)
        {
            return self.ChatMessageQueue.Count;
        }

        public static ChatInfo GetChatMessageByIndex(this ChatComponent self, int index)
        {
            int tempIndex = 0;
            foreach (var info in self.ChatMessageQueue)
            {
                if (tempIndex == index)
                {
                    return info;
                }

                tempIndex++;
            }
            
            return null;
        }

        public static void Add(this ChatComponent self, ChatInfo chatInfo)
        {
            //最多驻留100条
            if (self.ChatMessageQueue.Count >= 100)
            {
                var oldMessage = self.ChatMessageQueue.Dequeue();
                oldMessage?.Dispose();
            }
            
            self.ChatMessageQueue.Enqueue(chatInfo);
        }
    }
}