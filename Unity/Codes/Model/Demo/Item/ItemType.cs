namespace ET
{
    /// <summary>
    /// 物品项类型
    /// </summary>
    public enum ItemType
    {
        Weapon = 0, //武器
        Armor = 1,  //防具
        Ring = 2,   //戒指
        Prop = 3    //道具
    }

    /// <summary>
    /// 物品操作指令
    /// </summary>
    public enum ItemOp
    {
        Add = 0,
        Remove = 1
    }

    public enum ItemContainerType
    {
        Bag = 0,
        RoleInfo = 1
    }

    /// <summary>
    /// 物品品质
    /// </summary>
    public enum ItemQuality
    {
        General = 0,
        Good = 1,
        Excellent = 2,
        Epic = 3,
        Legand = 4
    }
}