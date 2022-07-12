using System.Collections.Generic;
#if SERVER
using MongoDB.Bson.Serialization.Attributes; 
#endif

namespace ET
{
    [ChildType(typeof(Item))]
#if SERVER
    public class EquipmentsComponent : Entity,IAwake,IDestroy,IDeserialize,IUnitCache,ITransfer
#else
    public class EquipmentsComponent : Entity,IAwake,IDestroy
#endif
    
    {
#if SERVER
        [BsonIgnore]
#endif
        public Dictionary<int, Item> EquipItems = new Dictionary<int, Item>();

#if SERVER
        [BsonIgnore]
        public M2C_ItemUpdateOpInfo message = new M2C_ItemUpdateOpInfo() { ContainerType = (int)ItemContainerType.RoleInfo };
#endif

    }
}