namespace ET
{
    public enum RoleInfoState
    {
        Normal = 0,
        Freeze,
    }
    
    [ComponentOf]
    
#if SERVER
    public class RoleInfo : Entity,IAwake,ITransfer,IUnitCache
#else
    public class RoleInfo : Entity,IAwake
#endif
    {
        public string NickName;

        public int State;

        public long AccountId;

        public long LastLoginTime;

        public long CreateTime;
    }
}