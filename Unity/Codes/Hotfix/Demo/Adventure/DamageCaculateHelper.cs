using System;

namespace ET
{
    public static class DamageCaculateHelper
    {
        public static int CaculateDamageValue(Unit attackUnit, Unit TargetUnit,ref SRandom random)
        {
            NumericComponent numericComponent = attackUnit.GetComponent<NumericComponent>();
            int damage = numericComponent.GetAsInt(NumericType.DamageValue);
            int dodge = TargetUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.Dodge);
            int aromr = TargetUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.ArmorValue);
            
            //随机 0 -100% 根据敏捷值闪避
            int rate = random.Range(0, 1000000);
            Log.Debug($"Rate {rate}");
            if (rate < dodge)
            {
                Log.Debug("闪避成功");
                damage = 0;
            }

            if (damage > 0)
            {
                //减去护甲值
                damage -= (int)Math.Ceiling(aromr * 0.08f);
                damage = damage < 0? 1 : damage;
            }

            return damage;
        }
    }
}