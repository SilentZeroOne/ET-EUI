namespace ET
{
    public partial class UnitConfigCategory
    {
        public UnitConfig GetPlayerConfig()
        {
            foreach (var config in Instance.list)
            {
                if (config.Type == (int) UnitType.Player)
                {
                    return config;
                }
            }

            return null;
        }
    }
}