namespace ET
{
	public class AI_ZhunBei: AAIHandler
	{
		public override int Check(AIComponent aiComponent, AIConfig aiConfig)
		{
			var currentScene = aiComponent.ZoneScene().CurrentScene();
			var landRoomComponent = currentScene.GetComponent<LandRoomComponent>();

			if (landRoomComponent == null) return 1;

			return (landRoomComponent.SelfIsReady || landRoomComponent.InReady)? 1 : 0;
		}

		public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
		{
			var zoneScene = aiComponent.ZoneScene();

			var myUnit = UnitHelper.GetMyUnitFromZoneScene(zoneScene);

			var errorCode = await MatchHelper.SetReadyNetwork(zoneScene, myUnit.Id, 1);
			if (errorCode != ErrorCode.ERR_Success)
			{
				Log.Error(errorCode.ToString());
			}
		}
	}
}