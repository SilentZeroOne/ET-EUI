using ET.EventType;

namespace ET
{
    public class AdventureBattleRoundEvent_CaculateDamage: AEvent<AdventureBattleRound>
    {
        protected override void Run(AdventureBattleRound a)
        {
            if (!a.AttackUnit.IsAlive() || !a.TargetUnit.IsAlive())
            {
                return;
            }

            int damage = a.AttackUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.DamageValue);

            int hp = a.TargetUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.Hp);

            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                a.TargetUnit.SetAlive(false);
            }

            a.TargetUnit.GetComponent<NumericComponent>().Set(NumericType.Hp, hp);
            
            Log.Debug($"************{a.AttackUnit.Type} 攻击造成 {damage}点伤害");
            Log.Debug($"************{a.TargetUnit.Type} 剩余血量 {hp}");
        }
    }
}