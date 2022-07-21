using ET.EventType;

namespace ET
{
    public class UpdateTaskInfoEvent_ShowRedDot : AEvent<UpdateTaskInfo>
    {
        protected override void Run(UpdateTaskInfo a)
        {
            bool isExist = a.ZoneScene.GetComponent<TaskComponent>().IsExistTaskComplete();
            if (isExist)
            {
                RedDotHelper.ShowRedDotNode(a.ZoneScene, "Task");
            }
            else
            {
                if (RedDotHelper.IsLogicAlreadyShow(a.ZoneScene, "Task"))
                {
                    RedDotHelper.HideRedDotNode(a.ZoneScene, "Task");
                }
            }
            
            a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgTask>()?.Refresh();
        }
    }
}