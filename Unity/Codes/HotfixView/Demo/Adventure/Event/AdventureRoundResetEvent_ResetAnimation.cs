using ET.EventType;

namespace ET
{
    public class AdventureRoundResetEvent_ResetAnimation : AEventAsync<AdventureRoundReset>
    {
        protected override async ETTask Run(AdventureRoundReset args)
        {
            Unit unit = UnitHelper.GetMyUnitFromZoneScene(args.ZoneScene);
            unit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Idle);

            await ETTask.CompletedTask;
        }
    }
}