using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class Item: Entity, IAwake<int>, IDestroy
    {
        public int ConfigId;

        [BsonIgnore]
        public ItemConfig Config => ItemConfigCategory.Instance.Get(this.ConfigId);
    }
}