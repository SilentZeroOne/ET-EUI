using UnityEngine;

namespace ET
{
    public enum MonoItemType
    {
        Seed,//种子
        Commodity,//货物
        Furniture,//家具
        HoeTool,//锄地工具
        ChopTool,//砍树工具
        BreakTool,//砸石头工具
        ReapTool,//割草工具
        WaterTool,//浇水工具
        CollectionTool,//收割工具
        ReapableScenary,//可被割的
    }
    
    [System.Serializable]
    public class ItemDetails
    {
        public int ItemId;

        public string ItemName;

        public MonoItemType ItemType;

        public Sprite ItemIcon;

        public Sprite ItemOnWorldSprite;

        public string ItemDescription;

        public int ItemUseRadius;

        public bool CanPickedUp;

        public bool CanDropped;

        public bool CanCarried;

        public int ItemPrice;

        [Range(0,1)]
        public float SellPercentage;
    }
}