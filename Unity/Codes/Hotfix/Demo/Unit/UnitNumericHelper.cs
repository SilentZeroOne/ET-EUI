namespace ET
{
    public static class UnitNumericHelper
    {
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