using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;

namespace ET
{
    [ProtoContract]
    public partial class ItemListProto : Object
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

        [ProtoMember(3)]
        public int IndexInInventory;

        [ProtoMember(4)]
        public ProtoVector3 WorldPosition;

    }

    [ProtoContract]
    public partial class ProtoVector3: Object
    {
        [ProtoMember(1)]
        public float X;

        [ProtoMember(2)]
        public float Y;
        
        [ProtoMember(3)]
        public float Z;

        public ProtoVector3()
        {
            
        }
        
        public ProtoVector3(Vector3 vector3)
        {
            this.X = vector3.x;
            this.Y = vector3.y;
            this.Z = vector3.z;
        }

        public ProtoVector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }
    }
}