namespace ET
{
	[MessageHandler]
	public class M2C_RemoveUnitsHandler : AMHandler<M2C_RemoveUnits>
	{
		protected override void Run(Session session, M2C_RemoveUnits message)
		{
			var zoneScene = session.DomainScene().ZoneScene();
			var currentScene = zoneScene?.CurrentScene();
			
			UnitComponent unitComponent = currentScene?.GetComponent<UnitComponent>();
			LandRoomComponent landRoomComponent = currentScene?.GetComponent<LandRoomComponent>();
			RoleInfoComponent roleInfo = zoneScene?.GetComponent<RoleInfoComponent>();
			
			foreach (long unitId in message.Units)
			{
				unitComponent?.Remove(unitId);
				roleInfo?.Remove(unitId);
				landRoomComponent?.RemoveUnit(unitId);
			}
		}
	}
}
