using ET.EventType;

namespace ET
{
    [NumericWatcher(NumericType.Level)]
    public class NumericWatcher_UpLevel : INumericWatcher
    {
        public void Run(NumbericChange args)
        {
            if (!(args.Parent is Unit unit))
            {
                return;
            }
            
            unit = args.Parent as Unit;
            unit.GetComponent<TaskComponent>().TriggerTaskAction(TaskActionType.UpLevel, (int)args.New);
        }
    }
}