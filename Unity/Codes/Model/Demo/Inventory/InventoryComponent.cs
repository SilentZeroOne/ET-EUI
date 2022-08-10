using System.Collections.Generic;
using ProtoBuf;

namespace ET
{
    [ComponentOf()]
    [ChildType(typeof(Item))]
    public class InventoryComponent: Entity, IAwake, IDestroy,IDeserialize
    {
        /// <summary>
        /// ItemId与Item
        /// </summary>
        [ProtoIgnore]
        public Dictionary<long, Item> ItemDict = new Dictionary<long, Item>();

        /// <summary>
        /// ItemType与Item
        /// </summary>
        [ProtoIgnore]
        public MultiMap<int, Item> ItemMap = new MultiMap<int, Item>();
        
        /// <summary>
        /// ItemConfigId与Item
        /// </summary>
        [ProtoIgnore]
        public MultiMap<int, Item> ItemConfigIdMap = new MultiMap<int, Item>();

        /// <summary>
        /// ConfigId列表
        /// </summary>
        public List<int> ItemConfigIdList = new List<int>();
    }
}