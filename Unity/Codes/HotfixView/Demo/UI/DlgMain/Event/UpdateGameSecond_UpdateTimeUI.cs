using ET.EventType;

namespace ET
{
    public class UpdateGameSecond_UpdateTimeUI : AEvent<UpdateGameSecond>
    {
        protected override void Run(UpdateGameSecond a)
        {
            a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().RefreshTimeUI(a.Time);
        }
    }
}