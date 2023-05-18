using ET.EventType;

namespace ET
{
	public class NumericChange_NoticeClient : AEventClass<NumbericChange>
	{
		protected override void Run(object a)
		{
			NumbericChange args = a as NumbericChange;
			if (!(args.Parent is Unit unit))
			{
				return;
			}
			
			unit.GetComponent<NumericNoticeComponent>()?.NoticeImmediately(args);
		}
	}
}