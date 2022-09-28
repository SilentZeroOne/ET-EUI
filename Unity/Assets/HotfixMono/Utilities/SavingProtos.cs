using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;

namespace ET
{
    [ProtoContract]
    public partial class TileProperty : Object
    {
        [ProtoMember(1)]
        public ProtoVector2Int TileCoordinate;

        [ProtoMember(2)]
        public int GridType;
        
        [ProtoMember(3)]
        public bool Value;
        
        [ProtoMember(4)]
        public int DaysSinceDug;
        [ProtoMember(5)]
        public int GrowthDays;
        [ProtoMember(6)]
        public MonoCropInfo Crop;
    }

    [ProtoContract]
    public partial class MapData: Object
    {
        [ProtoMember(1)]
        public List<TileProperty> Tiles = new List<TileProperty>();
    }
    
    [ProtoContract]
    public partial class MonoCropInfo: Object
    {
        [ProtoMember(1)]
        public int ConfigId;

        [ProtoMember(2)]
        public int LastStage;

        [ProtoMember(3)]
        public bool IsItem;
    }

    [ProtoContract]
    public partial class ProtoVector2Int: Object
    {
        [ProtoMember(1)]
        public int X;

        [ProtoMember(2)]
        public int Y;
        

        public ProtoVector2Int()
        {
            
        }
        
        public ProtoVector2Int(Vector2 vector3)
        {
            this.X = (int)vector3.x;
            this.Y = (int)vector3.y;
        }

        public ProtoVector2Int(float x, float y)
        {
            this.X = (int)x;
            this.Y = (int)y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }
    }
}