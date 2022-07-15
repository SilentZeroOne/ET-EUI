namespace ET
{
    [Timer(TimerType.MakeQueueOver)]
    public class MakeQueueOverTimer: ATimer<ForgeComponent>
    {
        public override void Run(ForgeComponent self)
        {
            Game.EventSystem.Publish(new EventType.MakeQueueOver() { ZoneScene = self.ZoneScene() });
        }
    }

    public class ForgeComponentAwakeSystem: AwakeSystem<ForgeComponent>
    {
        public override void Awake(ForgeComponent self)
        {

        }
    }

    public class ForgeComponentDestroySystem: DestroySystem<ForgeComponent>
    {
        public override void Destroy(ForgeComponent self)
        {

        }
    }

    [FriendClass(typeof(Production ))]
    [FriendClass(typeof (ForgeComponent))]
    public static class ForgeComponentSystem
    {
        public static void AddOrUpdateProductionQueue(this ForgeComponent self, ProductionProto message)
        {
            Production production = self.GetProductionById(message.Id);
            if (production == null)
            {
                production = self.AddChild<Production>();
                self.Productions.Add(production);
            }
            production.FromMessage(message);

            if (self.ProductionTimerDict.TryGetValue(production.Id, out long timerId))
            {
                TimerComponent.Instance.Remove(ref timerId);
                self.ProductionTimerDict.Remove(production.Id);
            }

            if (production.IsMakingState() && !production.IsMakeTimeOver())
            {
                Log.Debug($"启动一个定时器！！！：{production.TargetTime}");
                timerId = TimerComponent.Instance.NewOnceTimer(production.TargetTime, TimerType.MakeQueueOver, self);
                self.ProductionTimerDict.Add(production.Id, timerId);
            }

            Game.EventSystem.Publish(new EventType.MakeQueueOver() { ZoneScene = self.ZoneScene() });
        }

        public static Production GetProductionById(this ForgeComponent self, long productionId)
        {
            for (int i = 0; i < self.Productions.Count; i++)
            {
                if (self.Productions[i].Id == productionId)
                {
                    return self.Productions[i];
                }
            }

            return null;
        }

        public static Production GetProductionByIndex(this ForgeComponent self, int index)
        {
            for (int i = 0; i < self.Productions.Count; i++)
            {
                if (i == index)
                {
                    return self.Productions[i];
                }
            }

            return null;
        }

        public static bool IsExistMakeQueueOver(this ForgeComponent self)
        {
            return self.Productions.Count > 0;
        }

        public static int GetMakingProductionQueueCount(this ForgeComponent self)
        {
            var result = 0;
            for (int i = 0; i < self.Productions.Count; i++)
            {
                if (self.Productions[i].IsMakingState())
                {
                    result++;
                }
            }

            return result;
        }
    }
}