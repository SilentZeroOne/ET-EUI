
using System;

namespace ET
{
    [MessageHandler]
    public class Lo2C_NotifyUnitReadyHandler: AMHandler<Lo2C_NotifyUnitReady>
    {
        protected override void Run(Session session, Lo2C_NotifyUnitReady message)
        {
            LandRoomComponent landRoomComponent = session.ZoneScene().CurrentScene().GetComponent<LandRoomComponent>();
            landRoomComponent.SetReady(message.UnitId, message.Ready);
        }
    }
}