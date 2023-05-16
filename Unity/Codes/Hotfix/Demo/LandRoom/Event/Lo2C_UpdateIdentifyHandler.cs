

using System;
using ET.EventType;

namespace ET
{
	public class Lo2C_UpdateIdentifyHandler: AMHandler<Lo2C_UpdateIdentify>
	{
		protected override void Run(Session session, Lo2C_UpdateIdentify message)
		{
			Unit myUnit = UnitHelper.GetMyUnitFromZoneScene(session.ZoneScene());
			if (message.UnitId == myUnit.Id)
			{
				myUnit.GetComponent<HandCardsComponent>().AccessIdentify = (Identify)message.Identify;
			}

			Game.EventSystem.Publish(new UpdateIdentify() { ZoneScene = session.ZoneScene(), UnitId = message.UnitId, Idengtify = message.Identify });
		}
	}
}