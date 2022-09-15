using ET.EventType;

namespace ET
{
    public class OnItemSelected_ResetSelectedItemConfigId: AEvent<OnItemSelected>
    {
        protected override void Run(OnItemSelected a)
        {
            if (!a.Carried)
            {
                a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().CurrentItemConfigId = 0;
            }
        }
    }
}