using ET.EventType;

namespace ET
{
    public class NumericChangeEvent_NoticeClient : AEventClass<EventType.NumbericChange>
    {
        protected override void Run(object numbericChange)
        {
            NumbericChange args = numbericChange as NumbericChange;
            
            if (!(args.Parent is Unit unit))
            {
                return;
            }

            unit.GetComponent<NumericNoticeComponent>()?.NoticeImmediately(args);
        }
    }
}