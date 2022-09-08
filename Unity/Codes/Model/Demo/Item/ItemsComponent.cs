using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(Item))]
    public class ItemsComponent: Entity, IAwake, IDestroy
    {
        [BsonIgnore]
        public List<Item> ItemList = new List<Item>();
    }
}