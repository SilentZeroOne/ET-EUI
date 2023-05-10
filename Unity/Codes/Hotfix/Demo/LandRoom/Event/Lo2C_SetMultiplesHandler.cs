

using System;

namespace ET
{
	public class Lo2C_SetMultiplesHandler: AMHandler<Lo2C_SetMultiples>
	{
		protected override void Run(Session session, Lo2C_SetMultiples message)
		{
			LandRoomComponent landRoomComponent = session.ZoneScene().CurrentScene().GetComponent<LandRoomComponent>();
			landRoomComponent.SetMultiples(message.Multiples);
		}
	}
}