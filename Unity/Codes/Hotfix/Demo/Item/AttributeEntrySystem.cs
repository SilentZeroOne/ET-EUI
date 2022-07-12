namespace ET
{

    public class AttributeEntryDestroySystem : DestroySystem<AttributeEntry>
    {
        public override void Destroy(AttributeEntry self)
        {
            self.Key = 0;
            self.Value = 0;
            self.Type = EntryType.Common;
        }
    }

    [FriendClass(typeof(AttributeEntry))]
    public static class AttributeEntrySystem
    {
        public static AttributeEntryProto ToMessage(this AttributeEntry self)
        {
            AttributeEntryProto proto = new AttributeEntryProto();
            proto.Id = self.Id;
            proto.Key = self.Key;
            proto.Value = self.Value;
            proto.EntryType = (int)self.Type;

            return proto;
        }

        public static void FromMessage(this AttributeEntry self, AttributeEntryProto message)
        {
            self.Id = message.Id;
            self.Key = message.Key;
            self.Value = message.Value;
            self.Type = (EntryType)message.EntryType;
        }
    }
}