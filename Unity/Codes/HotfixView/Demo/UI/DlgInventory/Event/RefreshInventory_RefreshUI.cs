using ET.EventType;

namespace ET
{
    public class RefreshInventory_RefreshUI : AEvent<RefreshInventory>
    {
        protected override void Run(RefreshInventory a)
        {
            UIComponent uiComponent = a.ZoneScene.GetComponent<UIComponent>();
            if (uiComponent.IsWindowVisible(WindowID.WindowID_Main))
            {
                uiComponent.GetDlgLogic<DlgMain>().Refresh().Coroutine();
            }
            
            if (uiComponent.IsWindowVisible(WindowID.WindowID_Inventory))
            {
                uiComponent.GetDlgLogic<DlgInventory>().RefreshSlots();
            }
        }
    }
}