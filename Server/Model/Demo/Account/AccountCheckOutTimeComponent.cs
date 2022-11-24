namespace ET
{
    [ComponentOf()]
    public class AccountCheckOutTimeComponent: Entity, IAwake<long>, IDestroy
    {
        public long AccountId;
        public long Timer;
    }
}