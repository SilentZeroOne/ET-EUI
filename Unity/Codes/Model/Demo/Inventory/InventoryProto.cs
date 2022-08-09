using ProtoBuf;

namespace ET
{
    [ProtoContract]
    public partial class InventoryProto : Object
    {
        [ProtoMember(1)]
        public int RpcId { get; set; }

        [ProtoMember(2)]
        public int Error { get; set; }

        [ProtoMember(3)]
        public string Message { get; set; }
    }

    [ProtoContract]
    public partial class ItemInfo: Object
    {
        [ProtoMember(1)]
        public int ConfigId;
    }
}