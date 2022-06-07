namespace ET
{
    public static class UnitHelper
    {
        public static Unit GetMyUnitFromZoneScene(Scene zoneScene)
        {
            PlayerComponent playerComponent = zoneScene.GetComponent<PlayerComponent>();
            Scene currentScene = zoneScene.GetComponent<CurrentScenesComponent>().Scene;
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }
        
        public static Unit GetMyUnitFromCurrentScene(Scene currentScene)
        {
            PlayerComponent playerComponent = currentScene.Parent.Parent.GetComponent<PlayerComponent>();
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }

        public static NumericComponent GetMyUnitNumericComponent(Scene currentScene)
        {
            return GetMyUnitFromCurrentScene(currentScene).GetComponent<NumericComponent>();
        }

        public static bool IsAlive(this Unit self)
        {
            if (self == null || self.IsDisposed)
            {
                return false;
            }

            NumericComponent numericComponent = self.GetComponent<NumericComponent>();

            if (numericComponent == null)
            {
                return false;
            }

            return numericComponent.GetAsInt(NumericType.IsAlive) == 1;
        }

        public static void SetAlive(this Unit self, bool isAlive)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }

            NumericComponent numericComponent = self.GetComponent<NumericComponent>();

            if (numericComponent == null)
            {
                return;
            }
            
            numericComponent.Set(NumericType.IsAlive, isAlive? 1 : 0);
        }
    }
}