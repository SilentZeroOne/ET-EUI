namespace ET
{
    public enum SessionState
    {
        Normal,
        Game
    }
    
    [ComponentOf(typeof(Scene))]
    public class SessionStateComponent: Entity, IAwake, IDestroy
    {
        public SessionState State { get; set; }
    }
}