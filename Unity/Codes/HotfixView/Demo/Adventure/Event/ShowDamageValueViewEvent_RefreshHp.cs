using ET.EventType;

namespace ET
{
    public class ShowDamageValueViewEvent_RefreshHp : AEventAsync<ShowDamageValueView>
    {
        protected override async ETTask Run(ShowDamageValueView a)
        {
            a.TargetUnit.GetComponent<HeadHpViewComponent>().SetHp();
            a.ZoneScene.GetComponent<FlyDamageValueViewComponent>().SpawnFlyDamage(a.TargetUnit.Position,a.DamageValue).Coroutine();
            bool isAlive = a.TargetUnit.IsAlive();
            await TimerComponent.Instance.WaitAsync(400);
            
            a.TargetUnit?.GetComponent<HeadHpViewComponent>().SetVisiable(isAlive);
        }
    }
}