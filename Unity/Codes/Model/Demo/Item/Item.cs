using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class Item: Entity, IAwake<int>, IDestroy, ISerializeToEntity
    {
        public int ConfigId;

        [BsonIgnore]
        public ItemConfig Config => ItemConfigCategory.Instance.Get(this.ConfigId);
    }
}