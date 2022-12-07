namespace ET
{
    [FriendClass(typeof(Player))]
    public static class PlayerSystem
    {
        [ObjectSystem]
        public class PlayerAwakeSystem : AwakeSystem<Player, long>
        {
            public override void Awake(Player self, long accountId)
            {
                self.AccountId = accountId;
            }
        }
        
        public class PlayerDestroySystem: DestroySystem<Player>
        {
            public override void Destroy(Player self)
            {
                self.AccountId = 0;
                self.ClientSession?.Dispose();
                self.PlayerState = PlayerState.Disconnect;
            }
        }
    }
}