using ET.EventType;

namespace ET
{
    public class UpdateGameDay_UpdateDayUI : AEvent<UpdateGameDay>
    {
        protected override void Run(UpdateGameDay a)
        {
            a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().RefreshDayText(a.Time);
        }
    }
}