
using System;

namespace ET
{
    [MessageHandler]
    public class Lo2C_UpdateLandMatcherHandler: AMHandler<Lo2C_UpdateLandMatcher>
    {
        protected override void Run(Session session, Lo2C_UpdateLandMatcher message)
        {
            session.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgMatch>().UpdateMatchingCount(message.CurrentQueueCount.ToString());
        }
    }
}