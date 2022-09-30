using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;

namespace ET
{
    #region Item
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
    
    #endregion

    [ProtoContract]
    public partial class CropInfo: Object
    {
        [ProtoMember(1)]
        public int ConfigId;

        [ProtoMember(2)]
        public long CropId;

        [ProtoMember(3)]
        public int LastStage;
        
        [ProtoMember(4)]
        public int HarvestActionCount;
    }
    
    
    #region Grid
    [ProtoContract]
    public partial class TileDetails: Object
    {
        [ProtoMember(1)]
        public int GridX;
        [ProtoMember(2)]
        public int GridY;
        [ProtoMember(3)]
        public bool CanDig;
        [ProtoMember(4)]
        public bool CanDropItem;
        [ProtoMember(5)]
        public bool CanPlaceFurniture;
        [ProtoMember(6)]
        public bool IsNPCObstacle;
        [ProtoMember(7)]
        public int DaysSinceDug;
        [ProtoMember(8)]
        public int DaysSinceWatered;
        [ProtoMember(9)]
        public int GrowthDays;
        [ProtoMember(10)]
        public int DaysSinceLastHarvest;
        [ProtoMember(11)]
        public CropInfo Crop;
    }

    [ProtoContract]
    public partial class SavedMapData: Object
    {
        [ProtoMember(1)]
        public List<TileDetails> TileDetailsList = new List<TileDetails>();
        
        [ProtoMember(2)]
        public ProtoVector3 GridSize;
        
        [ProtoMember(3)]
        public ProtoVector3 StartNode;
    }

    #endregion
    
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