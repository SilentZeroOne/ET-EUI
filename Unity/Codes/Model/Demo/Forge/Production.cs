namespace ET
{
#if SERVER
    public class Production:Entity,IAwake,IDestroy,ISerializeToEntity
#else
    public class Production: Entity, IAwake, IDestroy
#endif
    {
        public long StartTime;
        public long TargetTime;
        public int ConfigId;
        public int ProductionState;
    }

    public enum ProductionConsumType
    {
        IronStone = 1, //精铁
        Fur = 2, //皮毛
    }

    public enum ProductionState
    {
        Received = 1, //已领取
        Making =2,//制造中
    }
}