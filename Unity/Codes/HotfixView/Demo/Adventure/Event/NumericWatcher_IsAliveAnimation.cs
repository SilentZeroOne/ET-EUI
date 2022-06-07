using ET.EventType;

namespace ET
{
    [NumericWatcher(NumericType.IsAlive)]
    public class NumericWatcher_IsAliveAnimation: INumericWatcher
    {
        public void Run(NumbericChange args)
        {
            if (!(args.Parent is Unit unit))
            {
                return;
            }

            unit.GetComponent<AnimatorComponent>()?.Play(args.New == 0? MotionType.Die : MotionType.Idle);
        }
    }
}