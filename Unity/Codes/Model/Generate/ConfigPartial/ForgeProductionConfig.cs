namespace ET
{
    public partial class ForgeProductionConfigCategory
    {
        public int GetProductionConfigCount(int unitLevel)
        {
            int count = 0;
            foreach (var config in this.list)
            {
                if (config.NeedLevel <= unitLevel)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count;
        }

        public ForgeProductionConfig GetProductionConfigByLevelIndex(int unitLevel, int index)
        {
            int tempIndex = 0;
            foreach (var config in this.list)
            {
                if (config.NeedLevel <= unitLevel)
                {
                    if (index == tempIndex)
                    {
                        return config;
                    }

                    tempIndex++;
                }
            }

            return null;
        }
    }
}