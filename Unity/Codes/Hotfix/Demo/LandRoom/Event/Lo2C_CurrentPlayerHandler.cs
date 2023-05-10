

using System;
using ET.EventType;

namespace ET
{
	public class Lo2C_CurrentPlayerHandler: AMHandler<Lo2C_CurrentPlayer>
	{
		protected override void Run(Session session, Lo2C_CurrentPlayer message)
		{
			var myUnit = UnitHelper.GetMyUnitFromZoneScene(session.ZoneScene());
			if (message.UnitId != myUnit.Id) return;

			var playingCard = false;
			switch ((ActionType)message.ActionType)
			{
				case ActionType.RobLandLord:
					playingCard = false;
					break;
				case ActionType.PlayCard:
					playingCard = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			EventSystem.Instance.Publish(new StartPlayCard() { ZoneScene = session.ZoneScene() ,PlayingCard = playingCard});
		}
	}
}