using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    public partial class PlayerNumericConfigCategory: ProtoObject, IMerge
    {
        public PlayerNumericConfig GetConfigByIndex(int index)
        {
            if (index < 0 || index > this.list.Count)
            {
                Log.Error($"Get Player numeric config index error {index}");
                return null;
            }
            return this.list[index];
        }

        public int GetShowConfigCount()
        {
            var count = 0;
            foreach (var config in this.list)
            {
                if (config.isNeedShow == 1)
                {
                    count++;
                }
            }

            return count;
        }
    }
}