using System.Collections.Generic;

namespace ET
{
    [ComponentOf()]
    [ChildType(typeof(Item))]
    public class InventoryComponent: Entity, IAwake, IDestroy
    {
        /// <summary>
        /// ItemId与Item
        /// </summary>
        public Dictionary<long, Item> ItemDict = new Dictionary<long, Item>();

        /// <summary>
        /// ItemType与Item
        /// </summary>
        public MultiMap<int, Item> ItemMap = new MultiMap<int, Item>();
    }
}