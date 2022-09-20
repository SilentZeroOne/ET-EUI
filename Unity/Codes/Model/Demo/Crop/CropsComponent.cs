using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(Crop))]
    public class CropsComponent: Entity, IAwake, IDestroy
    {
        [BsonIgnore]
        public List<Crop> CropList = new List<Crop>();
    }
}