using ET.EventType;

namespace ET
{
    public class BattleWinEvent_TaskUpdate : AEvent<BattleWin>
    {
        protected override void Run(BattleWin a)
        {
            a.Unit.GetComponent<TaskComponent>().TriggerTaskAction(TaskActionType.Adventure, 1, a.LevelId);
        }
    }
}