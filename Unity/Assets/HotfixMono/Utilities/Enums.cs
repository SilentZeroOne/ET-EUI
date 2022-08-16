namespace ET
{
    public enum ItemType
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

    public static class EnumsHelper
    {
        public static string GetName(this ItemType self)
        {
            return self switch
            {
                ItemType.Commodity => "商品",
                ItemType.Furniture => "家具",
                ItemType.Seed => "种子",
                ItemType.BreakTool => "工具",
                ItemType.ChopTool => "工具",
                ItemType.CollectionTool => "工具",
                ItemType.HoeTool => "工具",
                ItemType.ReapableScenary => "杂草",
                ItemType.ReapTool => "工具",
                ItemType.WaterTool => "工具",
                _ => "无",
            };
        }
    }
}