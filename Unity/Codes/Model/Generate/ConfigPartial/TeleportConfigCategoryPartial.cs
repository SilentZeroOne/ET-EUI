using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    public partial class TeleportConfigCategory
    {
        [ProtoIgnore]
        [BsonIgnore]
        private MultiMap<string, TeleportConfig> nameDict = new MultiMap<string, TeleportConfig>();

        public override void AfterEndInit()
        {
            foreach (var config in this.list)
            {
                this.nameDict.Add(config.SceneName, config);
            }
            
            base.AfterEndInit();
        }

        public TeleportConfig GetConfigBySceneNameAndPointName(string sceneName, string pointName)
        {
            this.nameDict.TryGetValue(sceneName, out var configList);
            if (configList != null)
            {
                foreach (var config in configList)
                {
                    if (config.PointName == pointName)
                    {
                        return config;
                    }
                }
            }

            return null;
        }
    }
}