using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

namespace ET
{
    public class Item: Entity,IAwake, IAwake<int>, IDestroy, ISerializeToEntity
    {
        public int ConfigId;

        [BsonIgnore]
        public ItemConfig Config => ItemConfigCategory.Instance.Get(this.ConfigId);

        public Vector3 Position { get; set; }
    }
}