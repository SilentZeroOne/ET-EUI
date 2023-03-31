using ET.EventType;

namespace ET
{
	public class SceneChangFinish_AddComponent : AEventAsync<SceneChangeFinish>
	{
		protected override async ETTask Run(SceneChangeFinish a)
		{
			
			await ETTask.CompletedTask;
		}
	}
}