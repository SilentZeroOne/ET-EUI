using ET.EventType;

namespace ET
{
    public class MakeQueueOverEvent_ShowRedDot : AEvent<MakeQueueOver>
    {
        protected override void Run(MakeQueueOver a)
        {
            bool isExist = a.ZoneScene.GetComponent<ForgeComponent>().IsExistMakeQueueOver();
            if (isExist)
            {
                RedDotHelper.ShowRedDotNode(a.ZoneScene, "Forge");
            }
            else
            {
                if (RedDotHelper.IsLogicAlreadyShow(a.ZoneScene, "Forge"))
                {
                    RedDotHelper.HideRedDotNode(a.ZoneScene, "Forge");
                }
            }
        }
    }
}