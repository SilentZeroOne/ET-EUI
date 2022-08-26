using ET.EventType;

namespace ET
{
    public class UpdateGameHour_UpdateSunRiseImage: AEvent<UpdateGameHour>
    {
        protected override void Run(UpdateGameHour a)
        {
            a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().RefreshSunRiseImage(a.Time);
        }
    }
}