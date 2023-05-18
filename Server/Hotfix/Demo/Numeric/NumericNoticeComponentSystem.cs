namespace ET
{
	public class NumericNoticeComponentAwakeSystem: AwakeSystem<NumericNoticeComponent>
	{
		public override void Awake(NumericNoticeComponent self)
		{

		}
	}

	public class NumericNoticeComponentDestroySystem: DestroySystem<NumericNoticeComponent>
	{
		public override void Destroy(NumericNoticeComponent self)
		{

		}
	}

	[FriendClass(typeof (NumericNoticeComponent))]
	public static class NumericNoticeComponentSystem
	{
		public static void NoticeImmediately(this NumericNoticeComponent self, EventType.NumbericChange args)
		{
			Unit unit = self.GetParent<Unit>();
			self.NoticeNumeric.UnitId = unit.Id;
			self.NoticeNumeric.NumericType = args.NumericType;
			self.NoticeNumeric.NewValue = args.New;
			MessageHelper.SendToClient(unit, self.NoticeNumeric);
		}
	}
}