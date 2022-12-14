
using System;

namespace ET
{
    [MessageHandler]
    public class M2C_UpdateLandMatcherHandler: AMHandler<M2C_UpdateLandMatcher>
    {
        protected override void Run(Session session, M2C_UpdateLandMatcher message)
        {
            session.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgMatch>().UpdateMatchingCount(message.CurrentQueueCount.ToString());
        }
    }
}