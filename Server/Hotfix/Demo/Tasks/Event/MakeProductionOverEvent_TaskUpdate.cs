using ET.EventType;

namespace ET
{
    public class MakeProductionOverEvent_TaskUpdate : AEvent<MakeProductionOver>
    {
        protected override void Run(MakeProductionOver a)
        {
            a.Unit.GetComponent<TaskComponent>().TriggerTaskAction(TaskActionType.MakeItem, 1, a.ProductionConfigId);
        }
    }
}