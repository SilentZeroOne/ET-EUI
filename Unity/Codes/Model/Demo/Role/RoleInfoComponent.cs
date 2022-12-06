namespace ET
{
    [ComponentOf()]
    [ChildType(typeof(RoleInfo))]
    public class RoleInfoComponent: Entity, IAwake, IDestroy
    {
        public RoleInfo RoleInfo { get; set; }
    }
}