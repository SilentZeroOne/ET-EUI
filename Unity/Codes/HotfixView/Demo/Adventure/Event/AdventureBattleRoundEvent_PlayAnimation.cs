using ET.EventType;
using UnityEngine;

namespace ET
{
    public class AdventureBattleRoundEvent_PlayAnimation : AEventAsync<AdventureBattleRound>
    {
        protected override async ETTask Run(AdventureBattleRound a)
        {
            if (!a.AttackUnit.IsAlive() || !a.TargetUnit.IsAlive())
            {
                return;
            }
            
            a.AttackUnit?.GetComponent<AnimatorComponent>().Play(MotionType.Attack);
            await TimerComponent.Instance.WaitAsync(300);
            a.TargetUnit?.GetComponent<AnimatorComponent>().Play(MotionType.Hurt);

            long instanceId = a.TargetUnit.InstanceId;
            
            a.TargetUnit.GetComponent<GameObjectComponent>().SpriteRenderer.color = Color.red;

            await TimerComponent.Instance.WaitAsync(300);

            if (instanceId != a.TargetUnit.InstanceId)
            {
                return;
            }
            
            a.TargetUnit.GetComponent<GameObjectComponent>().SpriteRenderer.color=Color.white;
            a.ZoneScene.GetComponent<ObjectWait>().Notify(new WaitType.Wait_PlayBattleAnimationEnd());
        }
    }
}