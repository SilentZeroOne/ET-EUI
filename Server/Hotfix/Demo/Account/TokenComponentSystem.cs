namespace ET
{
    [FriendClass(typeof (TokenComponent))]
    public static class TokenComponentSystem
    {
        public static void Add(this TokenComponent self,long accountId,string token)
        {
            self.TokenDictionary.Add(accountId, token);
            self.TimeOutRemoveKey(accountId, token).Coroutine();
        }

        public static string Get(this TokenComponent self, long accountId)
        {
            self.TokenDictionary.TryGetValue(accountId, out var token);
            return token;
        }

        public static void Remove(this TokenComponent self, long accountId)
        {
            if (self.TokenDictionary.ContainsKey(accountId))
            {
                self.TokenDictionary.Remove(accountId);
            }
        }
        
        private static async ETTask TimeOutRemoveKey(this TokenComponent self, long key, string token)
        {
            await TimerComponent.Instance.WaitAsync(60 * 1000 * 10);
            var onlineToken = self.Get(key);
            if (!string.IsNullOrEmpty(onlineToken) && onlineToken == token)
            {
                self.Remove(key);
            }
        }
    }
}