using System.Collections.Generic;
using ProtoBuf;

namespace ET
{
    [ComponentOf()]
    [ChildType(typeof(Item))]
    public class InventoryComponent: Entity, IAwake,IAwake<string,InventoryType>, IDestroy,IDeserialize
    {
        /// <summary>
        /// ItemId与Item
        /// </summary>
        public Dictionary<long, Item> ItemDict = new Dictionary<long, Item>();

        /// <summary>
        /// ItemType与Item
        /// </summary>
        public MultiMap<int, Item> ItemMap = new MultiMap<int, Item>();
        
        /// <summary>
        /// ItemConfigId与Item
        /// </summary>
        public MultiMap<int, Item> ItemConfigIdMap = new MultiMap<int, Item>();

        /// <summary>
        /// ConfigId列表
        /// </summary>
        public List<int> ItemConfigIdList = new List<int>();

        /// <summary>
        /// Index和ConfigId映射
        /// </summary>
        public DoubleMap<int, int> IndexConfigIdDict = new DoubleMap<int, int>();

        /// <summary>
        /// 存储位置
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// Inventory 类型
        /// </summary>
        public InventoryType Type { get; set; }
        
        public bool Loaded { get; set; }
    }

    public enum InventoryType
    {
        Inventory, //背包
        ActionBar, //快捷栏
    }
}