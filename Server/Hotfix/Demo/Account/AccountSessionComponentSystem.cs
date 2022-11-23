namespace ET
{
    public class AccountSessionComponentAwakeSystem: AwakeSystem<AccountSessionComponent>
    {
        public override void Awake(AccountSessionComponent self)
        {

        }
    }

    public class AccountSessionComponentDestroySystem: DestroySystem<AccountSessionComponent>
    {
        public override void Destroy(AccountSessionComponent self)
        {
            self.AccountSessionDictionary.Clear();
        }
    }
    

    [FriendClass(typeof (AccountSessionComponent))]
    public static class AccountSessionComponentSystem
    {
        public static void Add(this AccountSessionComponent self,long accountId,long sessionInstanceId)
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

        public static long Get(this AccountSessionComponent self,long accountId)
        {
            self.AccountSessionDictionary.TryGetValue(accountId, out var instanceId);
            return instanceId;
        }
        
        public static void Remove(this AccountSessionComponent self,long accountId)
        {
            if (self.AccountSessionDictionary.ContainsKey(accountId))
            {
                self.AccountSessionDictionary.Remove(accountId);
            }
        }
    }
}