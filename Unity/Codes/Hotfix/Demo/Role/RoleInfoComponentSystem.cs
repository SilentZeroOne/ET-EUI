namespace ET
{
	public class RoleInfoComponentAwakeSystem: AwakeSystem<RoleInfoComponent>
	{
		public override void Awake(RoleInfoComponent self)
		{

		}
	}

	public class RoleInfoComponentDestroySystem: DestroySystem<RoleInfoComponent>
	{
		public override void Destroy(RoleInfoComponent self)
		{

		}
	}

	[FriendClass(typeof (RoleInfoComponent))]
	public static class RoleInfoComponentSystem
	{
		public static RoleInfo GetSelfRoleInfo(this RoleInfoComponent self)
		{
			var unitId = self.ZoneScene().GetComponent<PlayerComponent>().MyId;
			return self.GetChild<RoleInfo>(unitId);
		}

		public static void Remove(this RoleInfoComponent self, long id)
		{
			var child = self.GetChild<Entity>(id);
			child?.Dispose();
		}
	}
}