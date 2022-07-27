namespace ET
{
    [ObjectSystem]
    public class PlayerAwakeSystem: AwakeSystem<Player, long, long>
    {
        public override void Awake(Player self, long accountId, long roleId)
        {
            self.Account = accountId;
            self.UnitId = roleId;
        }
    }
    
    public class PlayerDestroySystem: DestroySystem<Player>
    {
        public override void Destroy(Player self)
        {
            self.Account = 0;
            self.ClientSession?.Dispose();
            self.UnitId = 0;
            self.ChatInfoUnitInstanceId = 0;
            self.PlayerState = PlayerState.Disconnect;
        }
    }
    
    public static class PlayerSystem
    {
        
    }
}