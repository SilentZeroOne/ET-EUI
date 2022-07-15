using System.Collections.Generic;

#if SERVER
using MongoDB.Bson.Serialization.Attributes;
#endif

namespace ET
{
    [ChildType(typeof(Production))]
    [ComponentOf()]
#if SERVER
    public class ForgeComponent : Entity,IAwake,IDestroy,IDeserialize,ITransfer,IUnitCache
#else
    public class ForgeComponent : Entity, IAwake, IDestroy
#endif
    {
#if SERVER
        [BsonIgnore]
#endif
        public Dictionary<long, long> ProductionTimerDict = new Dictionary<long, long>();
        
#if SERVER
        [BsonIgnore]
#endif
        public List<Production> Productions = new List<Production>();
    }
}