namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class TeleportComponent: Entity, IAwake, IDestroy
    {
        public TriggerAction Trigger;
    }
}