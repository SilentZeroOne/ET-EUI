namespace ET
{
    public static class DamageCaculateHelper
    {
        public static int CaculateDamageValue(Unit attackUnit, Unit TargetUnit,ref SRandom random)
        {
            int damage = attackUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.DamageValue);

            return damage;
        }
    }
}