namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(Item))]
    public class ItemsComponent: Entity, IAwake, IDestroy
    {
        
    }
}