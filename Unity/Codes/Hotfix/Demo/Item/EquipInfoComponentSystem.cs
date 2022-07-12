namespace ET
{
    public class EquipInfoComponentDestroySystem : DestroySystem<EquipInfoComponent>
    {
        public override void Destroy(EquipInfoComponent self)
        {
            self.isInited = false;
            self.Score = 0;
            foreach (var entry in self.EntryList)
            {
                entry?.Dispose();
            }
            self.EntryList.Clear();
        }
    }
    
    
    [FriendClass(typeof(EquipInfoComponent))]
    [FriendClass(typeof(AttributeEntry))]
    public static class EquipInfoComponentSystem
    {
        public static void FromMessage(this EquipInfoComponent self, EquipInfoProto message)
        {
            self.Score = message.Score;
            for (int i = 0; i < self.EntryList.Count; i++)
            {
                self.EntryList[i]?.Dispose();
            }
            
            self.EntryList.Clear();

            for (int i = 0; i < message.AttributeEntryProtoList.Count; i++)
            {
                AttributeEntry attributeEntry = self.AddChild<AttributeEntry>();
                attributeEntry.FromMessage(message.AttributeEntryProtoList[i]);
                
                self.EntryList.Add(attributeEntry);
            }

            self.isInited = true;
        }
    }
}