

using System;
using ET.EventType;

namespace ET
{
	public class Lo2C_RobLandLordHandler: AMHandler<Lo2C_RobLandLord>
	{
		protected override void Run(Session session, Lo2C_RobLandLord message)
		{
			LandRoomComponent landRoomComponent = session.ZoneScene().CurrentScene().GetComponent<LandRoomComponent>();
			landRoomComponent.SetPromt(message.UnitId, message.Rob);
		}
	}
}