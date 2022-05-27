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
    
    public static class PlayerSystem
    {
        
    }
}