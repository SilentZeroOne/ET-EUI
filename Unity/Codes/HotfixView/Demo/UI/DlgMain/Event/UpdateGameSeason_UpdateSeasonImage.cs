using ET.EventType;

namespace ET
{
    public class UpdateGameSeason_UpdateSeasonImage: AEvent<UpdateGameSeason>
    {
        protected override void Run(UpdateGameSeason a)
        {
            a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().RefreshSeasonImage(a.Time);
        }
    }
}