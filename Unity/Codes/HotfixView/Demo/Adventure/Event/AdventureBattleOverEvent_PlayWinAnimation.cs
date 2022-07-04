using ET.EventType;

namespace ET
{
    public class AdventureBattleOverEvent_PlayWinAnimation: AEventAsync<AdventureBattleOver>
    {
        protected override async ETTask Run(AdventureBattleOver a)
        {
            a.WinUnit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Win);
            a.WinUnit?.GetComponent<HeadHpViewComponent>()?.SetVisiable(false);
            await ETTask.CompletedTask;
        }
    }
}