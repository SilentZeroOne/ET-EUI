using ET.EventType;

namespace ET
{
    public class OnItemSelected_SetCursor : AEvent<OnItemSelected>
    {
        protected override void Run(OnItemSelected a)
        {
            a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgCursor>().SetCursorImage(a.Carried? a.Item.Config.ItemType : -1);
        }
    }
}