using ET.EventType;

namespace ET
{
    [NumericWatcher(NumericType.Exp)]
    [NumericWatcher(NumericType.Gold)]
    [NumericWatcher(NumericType.Level)]
    public class NumericWatcher_RefreshUI : INumericWatcher
    {
        public void Run(NumbericChange args)
        {
            args.Parent.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgMain>()?.Refresh();
        }
    }
}