using ET.EventType;

namespace ET
{
    public class RefreshInventory_RefreshUI : AEvent<RefreshInventory>
    {
        protected override void Run(RefreshInventory a)
        {
            UIComponent uiComponent = a.ZoneScene.GetComponent<UIComponent>();
            if (uiComponent.IsWindowVisible(WindowID.WindowID_Inventory))
            {
                uiComponent.GetDlgLogic<DlgInventory>().RefreshSlots();
            }
        }
    }
}