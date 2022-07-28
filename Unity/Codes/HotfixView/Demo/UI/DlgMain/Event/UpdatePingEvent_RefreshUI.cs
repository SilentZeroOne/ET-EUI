using System;
using ET.EventType;

namespace ET
{
    public class UpdatePingEvent_RefreshUI : AEvent<UpdatePing>
    {
        protected override void Run(UpdatePing a)
        {
            a.ZoneScene?.GetComponent<UIComponent>().GetDlgLogic<DlgMain>()?.UpdatePingText(a.Ping);
        }
    }
}