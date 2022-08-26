using ET.EventType;

namespace ET
{
    public class UpdateGameMintue_UpdateTimeImage : AEvent<UpdateGameMinute>
    {
        protected override void Run(UpdateGameMinute a)
        {
            a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().RefreshTimeImage(a.Time);
        }
    }
}