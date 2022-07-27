namespace ET
{
    public class ChatInfoUnitsComponentAwakeSystem: AwakeSystem<ChatInfoUnitsComponent>
    {
        public override void Awake(ChatInfoUnitsComponent self)
        {

        }
    }

    public class ChatInfoUnitsComponentDestroySystem: DestroySystem<ChatInfoUnitsComponent>
    {
        public override void Destroy(ChatInfoUnitsComponent self)
        {

        }
    }

    [FriendClass(typeof (ChatInfoUnitsComponent))]
    public static class ChatInfoUnitsComponentSystem
    {
        public static ChatInfoUnit Get(this ChatInfoUnitsComponent self,long id)
        {
            self.ChatInfoUnitsDict.TryGetValue(id, out var unit);
            return unit;
        }

        public static void Add(this ChatInfoUnitsComponent self, ChatInfoUnit chatInfoUnit)
        {
            if (self.ChatInfoUnitsDict.ContainsKey(chatInfoUnit.Id))
            {
                Log.Error($"ChatInfoUnit already exist! :{chatInfoUnit.Id}");
                return;
            }

            self.ChatInfoUnitsDict.Add(chatInfoUnit.Id, chatInfoUnit);  
        }
        
        public static void Remove(this ChatInfoUnitsComponent self,long id)
        {
            if (self.ChatInfoUnitsDict.TryGetValue(id, out var unit))
            {
                self.ChatInfoUnitsDict.Remove(id);
                unit?.Dispose();
            }
        }
    }
}