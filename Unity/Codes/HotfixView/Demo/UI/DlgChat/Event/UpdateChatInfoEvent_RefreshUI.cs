using ET.EventType;

namespace ET
{
    public class UpdateChatInfoEvent_RefreshUI: AEvent<UpdateChatInfo>
    {
        protected override void Run(UpdateChatInfo a)
        {
            a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgChat>()?.Refresh();
        }
    }
}