using System.Collections.Generic;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    public partial class InventoryProto : Object
    {
        [ProtoMember(1)]
        public List<ItemInfo> ItemInfos = new List<ItemInfo>();
    }

    [ProtoContract]
    public partial class ItemInfo: Object
    {
        [ProtoMember(1)]
        public int ConfigId;

        [ProtoMember(2)]
        public long ItemId;
    }
}