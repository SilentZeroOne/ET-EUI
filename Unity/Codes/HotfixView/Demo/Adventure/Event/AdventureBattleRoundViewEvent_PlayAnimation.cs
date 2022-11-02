using ET.EventType;
using UnityEngine;

namespace ET
{
    public class AdventureBattleRoundViewEvent_PlayAnimation : AEventAsync<AdventureBattleRoundView>
    {
        protected override async ETTask Run(AdventureBattleRoundView a)
        {
            if (!a.AttackUnit.IsAlive() || !a.TargetUnit.IsAlive())
            {
                return;
            }
            
            a.AttackUnit?.GetComponent<AnimatorComponent>().Play(MotionType.Attack);
            a.TargetUnit?.GetComponent<AnimatorComponent>().Play(MotionType.Hurt);

            long instanceId = a.TargetUnit.InstanceId;
            
            await TimerComponent.Instance.WaitAsync(200);
            a.TargetUnit.GetComponent<GameObjectComponent>().SpriteRenderer.color = Color.red;

            await TimerComponent.Instance.WaitAsync(100);

            if (instanceId != a.TargetUnit.InstanceId)
            {
                return;
            }
            
            a.TargetUnit.GetComponent<GameObjectComponent>().SpriteRenderer.color=Color.white;
            a.ZoneScene.GetComponent<ObjectWait>().Notify(new WaitType.Wait_PlayBattleAnimationEnd());
        }
    }
}