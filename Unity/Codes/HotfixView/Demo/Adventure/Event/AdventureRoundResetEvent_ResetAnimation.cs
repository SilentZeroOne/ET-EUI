using ET.EventType;

namespace ET
{
    public class AdventureRoundResetEvent_ResetAnimation : AEvent<AdventureRoundReset>
    {
        protected override void Run(AdventureRoundReset args)
        {
            Unit unit = UnitHelper.GetMyUnitFromZoneScene(args.ZoneScene);
            unit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Idle);
        }
    }
}