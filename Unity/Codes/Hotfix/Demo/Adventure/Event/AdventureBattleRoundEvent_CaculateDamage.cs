using ET.EventType;

namespace ET
{
    public class AdventureBattleRoundEvent_CaculateDamage: AEventAsync<AdventureBattleRound>
    {
        protected override async ETTask Run(AdventureBattleRound a)
        {
            await a.ZoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_PlayBattleAnimationEnd>();
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
            Game.EventSystem.PublishAsync(new ShowDamageValueView()
            {
                ZoneScene = a.ZoneScene,TargetUnit = a.TargetUnit,DamageValue = damage
            }).Coroutine();
            
            Log.Debug($"************{a.AttackUnit.Type} 攻击造成 {damage}点伤害");
            Log.Debug($"************{a.TargetUnit.Type} 剩余血量 {hp}");

            await ETTask.CompletedTask;
        }
    }
}