namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class ItemPickerComponent: Entity, IAwake, IDestroy
    {
        public TriggerAction TriggerAction;
    }
}