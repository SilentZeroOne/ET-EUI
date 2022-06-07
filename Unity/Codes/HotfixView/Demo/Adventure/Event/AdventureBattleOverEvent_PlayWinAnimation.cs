using ET.EventType;

namespace ET
{
    public class AdventureBattleOverEvent_PlayWinAnimation: AEvent<AdventureBattleOver>
    {
        protected override void Run(AdventureBattleOver a)
        {
            a.WinUnit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Win);
        }
    }
}