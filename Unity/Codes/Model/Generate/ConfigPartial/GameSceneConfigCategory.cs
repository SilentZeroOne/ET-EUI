namespace ET
{
    public partial class GameSceneConfigCategory
    {
        public GameSceneConfig GetBySceneName(string name)
        {
            foreach (var config in this.list)
            {
                if (config.SceneName == name)
                    return config;
            }

            return null;
        }
    }
}