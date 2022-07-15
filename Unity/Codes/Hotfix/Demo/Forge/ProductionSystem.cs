namespace ET
{
    public class ProductionAwakeSystem: AwakeSystem<Production>
    {
        public override void Awake(Production self)
        {

        }
    }

    public class ProductionDestroySystem: DestroySystem<Production>
    {
        public override void Destroy(Production self)
        {

        }
    }

    [FriendClass(typeof (Production))]
    public static class ProductionSystem
    {
        public static void FromMessage(this Production self, ProductionProto proto)
        {
            self.Id = proto.Id;
            self.ConfigId = proto.ConfigId;
            self.ProductionState = proto.ProductionState;
            self.StartTime = proto.StartTime;
            self.TargetTime = proto.TargetTime;
        }

        public static ProductionProto ToMessage(this Production self)
        {
            return new ProductionProto()
            {
                Id = self.Id,
                ConfigId = self.ConfigId,
                StartTime = self.StartTime,
                ProductionState = self.ProductionState,
                TargetTime = self.TargetTime
            };
        }

        public static bool IsMakingState(this Production self)
        {
            return self.ProductionState == (int)ProductionState.Making;
        }

        public static bool IsMakeTimeOver(this Production self)
        {
            return TimeHelper.ServerNow() >= self.TargetTime;
        }
        
    }
}