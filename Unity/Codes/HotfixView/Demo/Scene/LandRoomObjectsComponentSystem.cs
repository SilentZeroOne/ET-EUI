using UnityEngine;

namespace ET
{
	public class LandRoomObjectsComponentAwakeSystem: AwakeSystem<LandRoomObjectsComponent>
	{
		public override void Awake(LandRoomObjectsComponent self)
		{
			self.GetAllObjects();
		}
	}

	public class LandRoomObjectsComponentDestroySystem: DestroySystem<LandRoomObjectsComponent>
	{
		public override void Destroy(LandRoomObjectsComponent self)
		{

		}
	}

	[FriendClass(typeof (LandRoomObjectsComponent))]
	public static class LandRoomObjectsComponentSystem
	{
		public static void GetAllObjects(this LandRoomObjectsComponent self)
		{
			var deskSprite = GameObject.Find("DeskSprite");
			self.DeskSprite = deskSprite.GetComponent<SpriteRenderer>();
			self.SelfUnitPosition = deskSprite.Get<GameObject>("SelfUnitPosition").transform;
			self.OtherUnitPositions = new[]
			{
				deskSprite.Get<GameObject>("OtherUnitPosition_1").transform, 
				deskSprite.Get<GameObject>("OtherUnitPosition_2").transform
			};

			self.ZoneScene().CurrentScene().GetComponent<ObjectWait>().Notify(new WaitType.Wait_PlayRoomAddObjectComponentFinish());
		}
	}
}