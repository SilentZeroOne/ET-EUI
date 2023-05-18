namespace ET
{
	[ComponentOf()]
	public class NumericNoticeComponent: Entity, IAwake, IDestroy
	{
		public Lo2C_NoticeNumeric NoticeNumeric = new Lo2C_NoticeNumeric();
	}
}