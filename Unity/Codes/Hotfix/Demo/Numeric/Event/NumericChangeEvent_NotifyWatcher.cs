namespace ET
{
    public class NumericChangeEvent_NotifyWatcher : AEventClass<EventType.NumbericChange>
    {
        protected override void Run(object numbericChange)
        {

            EventType.NumbericChange args = numbericChange as EventType.NumbericChange;
            
            NumericWatcherComponent.Instance.Run(args);
        }
    }
}