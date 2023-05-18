namespace ET
{
	[MessageHandler]
	public class M2C_CreateUnitsHandler : AMHandler<M2C_CreateUnits>
	{
		protected override void Run(Session session, M2C_CreateUnits message)
		{
			Scene currentScene = session.DomainScene().CurrentScene();
			UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
			
			foreach (UnitInfo unitInfo in message.Units)
			{
				var unit = unitComponent.Get(unitInfo.UnitId);
				if (unit != null)
				{
					unit.SeatIndex = unitInfo.SeatIndex;
					continue;
				}
				UnitFactory.Create(currentScene, unitInfo).Coroutine();
			}
		}
	}
}
