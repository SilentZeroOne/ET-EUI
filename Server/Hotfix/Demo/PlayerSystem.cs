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
    }
}