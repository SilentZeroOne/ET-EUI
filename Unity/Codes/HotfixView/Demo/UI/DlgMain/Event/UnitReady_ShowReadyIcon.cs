using ET.EventType;

namespace ET
{
    public class UnitReady_ShowReadyIcon : AEvent<UnitReady>
    {
        protected override void Run(UnitReady a)
        {
            a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>()?.SetReadyIcon(a.UnitIndex, a.Ready == 1);
        }
    }
}