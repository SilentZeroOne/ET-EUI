namespace ET
{
    public class NumericChangeEvent_NotifyWatcher : AEvent<EventType.NumbericChange>
    {
        protected override async ETTask Run(EventType.NumbericChange args)
        {
            NumericWatcherComponent.Instance.Run(args);
            await ETTask.CompletedTask;
        }
    }
}