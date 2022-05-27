using ProtoBuf;

namespace ET
{
    [Config]
    public partial class BattleLevelConfigCategory
    {
        public BattleLevelConfig GetConfigByIndex(int index)
        {
            if (index < 0 || index > this.list.Count)
            {
                Log.Error($"Get BattleLevelConfig index error {index}");
                return null;
            }
            
            return this.list[index];
        }

        public int GetShowConfigCount()
        {
            return this.list.Count;
        }
    }
}