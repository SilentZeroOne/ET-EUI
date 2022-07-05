using ET.EventType;

namespace ET
{
    [NumericWatcher(NumericType.Agility)]
    [NumericWatcher(NumericType.Stamina)]
    [NumericWatcher(NumericType.Strength)]
    [NumericWatcher(NumericType.Spirit)]
    public class NumericWatcher_AddAttributePoint : INumericWatcher
    {
        public void Run(NumbericChange args)
        {
            if (!(args.Parent is Unit unit))
            {
                return;
            }

            if (args.NumericType == NumericType.Strength)
            {
                unit.GetComponent<NumericComponent>()[NumericType.DamageValueAdd] += 5;
            }

            if (args.NumericType == NumericType.Agility)
            {
                unit.GetComponent<NumericComponent>()[NumericType.ArmorValueFinalAdd] += 5;
                unit.GetComponent<NumericComponent>()[NumericType.Dodge] += 5000;
            }

            if (args.NumericType == NumericType.Stamina)
            {
                unit.GetComponent<NumericComponent>()[NumericType.MaxHpPct] += 1 * 10000;
            }

            if (args.NumericType == NumericType.Spirit)
            {
                unit.GetComponent<NumericComponent>()[NumericType.MpFinalPct] += 1 * 10000;
            }
        }
    }
}