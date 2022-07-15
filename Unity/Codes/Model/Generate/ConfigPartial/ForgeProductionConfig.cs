namespace ET
{
    public partial class ForgeProductionConfigCategory
    {
        public int GetProductionConfigCount(int unitLevel)
        {
            int count = 0;
            foreach (var config in this.dict.Values)
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
    }
}