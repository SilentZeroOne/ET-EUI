namespace ET
{
    public class AccountSessionComponentAwakeSystem: AwakeSystem<AccountSessionsComponent>
    {
        public override void Awake(AccountSessionsComponent self)
        {

        }
    }

    public class AccountSessionComponentDestroySystem: DestroySystem<AccountSessionsComponent>
    {
        public override void Destroy(AccountSessionsComponent self)
        {
            self.AccountSessionDictionary.Clear();
        }
    }
    

    [FriendClass(typeof (AccountSessionsComponent))]
    public static class AccountSessionComponentSystem
    {
        public static void Add(this AccountSessionsComponent self,long accountId,long sessionInstanceId)
        {
            if (self.AccountSessionDictionary.ContainsKey(accountId))
            {
                self.AccountSessionDictionary[accountId] = sessionInstanceId;
            }
            else
            {
                self.AccountSessionDictionary.Add(accountId, sessionInstanceId);
            }
        }

        public static long Get(this AccountSessionsComponent self,long accountId)
        {
            self.AccountSessionDictionary.TryGetValue(accountId, out var instanceId);
            return instanceId;
        }
        
        public static void Remove(this AccountSessionsComponent self,long accountId)
        {
            if (self.AccountSessionDictionary.ContainsKey(accountId))
            {
                self.AccountSessionDictionary.Remove(accountId);
            }
        }
    }
}