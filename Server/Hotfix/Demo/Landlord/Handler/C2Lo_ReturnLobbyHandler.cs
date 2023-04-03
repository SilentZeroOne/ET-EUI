
using System;

namespace ET
{
	public class C2Lo_ReturnLobbyHandler: AMActorHandler<Scene, C2Lo_ReturnLobby>
	{
		protected override async ETTask Run(Scene scene, C2Lo_ReturnLobby message)
		{
			LandMatchComponent landMatchComponent = scene.GetComponent<LandMatchComponent>();
			if (landMatchComponent == null)
			{
				Log.Error(ErrorCode.ERR_RequestSceneTypeError.ToString());
				return;
			}
			
			Unit unit = landMatchComponent.RemoveUnit(message.UnitId);
			
			StartSceneConfig config = StartSceneConfigCategory.Instance.GetBySceneName(scene.DomainZone(), "Lobby");

			await TransferHelper.Transfer(unit, config.InstanceId, config.Name);
			
			await ETTask.CompletedTask;
		}
	}
}