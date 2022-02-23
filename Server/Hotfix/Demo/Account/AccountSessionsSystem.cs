namespace ET
{
    public class AccountSessionsDestroySystem: DestroySystem<AccountSessionsComponent>
    {
        public override void Destroy(AccountSessionsComponent self)
        {
            self.AccountSessionsDict.Clear();
        }
    }
    
    public static class AccountSessionsSystem
    {
        public static long Get(this AccountSessionsComponent self, long accountId)
        {
            self.AccountSessionsDict.TryGetValue(accountId, out var instanceId);
            return instanceId;
        }

        public static void Add(this AccountSessionsComponent self, long key, long instanceId)
        {
            if (self.AccountSessionsDict.ContainsKey(key))
            {
                self.AccountSessionsDict[key] = instanceId;
            }
            else
            {
                self.AccountSessionsDict.Add(key, instanceId);
            }
        }

        public static void Remove(this AccountSessionsComponent self, long key)
        {
            if (self.AccountSessionsDict.ContainsKey(key))
            {
                self.AccountSessionsDict.Remove(key);
            }
        }
    }
}