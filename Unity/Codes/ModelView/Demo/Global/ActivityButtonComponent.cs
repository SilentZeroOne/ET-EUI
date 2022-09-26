namespace ET
{
    [ComponentOf()]
    public class ActivityButtonComponent: Entity, IAwake, IAwake<int>, IDestroy, IUpdate
    {
        public int StartCode = 49;// Alpha1
        public int KeyCode;
    }
}