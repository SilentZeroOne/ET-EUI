using ET.EventType;

namespace ET
{
    public class ChangeUnitDir_SyncAnimator : AEvent<ChangeUnitDir>
    {
        protected override void Run(ChangeUnitDir a)
        {
            if (a.Unit.Config.Type == (int)UnitType.NPC)
            {
                var inputX = a.Dir.GetRawX();
                var inputY = a.Dir.GetRawY();

                if (inputX != 0 || inputY != 0)
                {
                    a.Unit.GetComponent<AnimatorComponent>().SetMoveParmas(inputX, inputY);
                    a.Unit.GetComponent<AnimatorComponent>().Play(MotionType.IsMoving);
                }
                else
                {
                    Stop(a).Coroutine();
                }
            }
        }

        private async ETTask Stop(ChangeUnitDir a)
        {
            await TimerComponent.Instance.WaitAsync(100);
            a.Unit.GetComponent<AnimatorComponent>().SetMoveParmas(0, -1);
            a.Unit.GetComponent<AnimatorComponent>().ForEveryAnimator(AnimatorControlType.ResetTrigger, MotionType.IsMoving.ToString());
        }
    }
}