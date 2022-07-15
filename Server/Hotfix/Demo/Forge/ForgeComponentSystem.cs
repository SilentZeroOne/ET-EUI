namespace ET
{
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

    public class ForgeComponentDeserializeSystem: DeserializeSystem<ForgeComponent>
    {
        public override void Deserialize(ForgeComponent self)
        {
            foreach (Entity entity in self.Children.Values)
            {
                self.Productions.Add(entity as Production);
            }
        }
    }

    [FriendClass(typeof(Production))]
    [FriendClass(typeof (ForgeComponent))]
    public static class ForgeComponentSystem
    {
        public static bool IsExistFreeQueue(this ForgeComponent self)
        {
            if (self.Productions.Count < 2)
            {
                return true;
            }

            Production production = self.GetFreeProduction();

            if (production != null)
            {
                return true;
            }

            return false;
        }

        public static Production GetFreeProduction(this ForgeComponent self)
        {
            for (int i = 0; i < self.Productions.Count; i++)
            {
                if (self.Productions[i].ProductionState == (int)ProductionState.Received)
                {
                    return self.Productions[i];
                }
            }

            return null;
        }

        public static Production StartProduction(this ForgeComponent self, int configId)
        {
            Production production = self.GetFreeProduction();
            if (production == null)
            {
                production = self.AddChild<Production>();
                self.Productions.Add(production);
            }

            production.ConfigId = configId;
            production.ProductionState = (int)ProductionState.Making;
            production.StartTime = TimeHelper.ServerNow();
            production.TargetTime = TimeHelper.ServerNow() + ForgeProductionConfigCategory.Instance.Get(configId).ProductionTime * 1000;

            return production;
        }

        
    }
}