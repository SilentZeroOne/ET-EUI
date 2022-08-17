using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    public partial class AnimatorControllerConfigCategory
    {
        [ProtoIgnore]
        [BsonIgnore]
        private MultiMap<string, AnimatorControllerConfig> nameDict = new MultiMap<string, AnimatorControllerConfig>();

        public override void AfterEndInit()
        {
            foreach (var config in this.list)
            {
                this.nameDict.Add(config.AnimatorName, config);
            }
            
            base.AfterEndInit();
        }

        public AnimatorControllerConfig GetConfigByNameAndStatus(string name, int animatorStatus)
        {
            this.nameDict.TryGetValue(name, out var configList);

            if (configList != null)
            {
                foreach (var config in configList)
                {
                    if (config.AniamtorStatus == animatorStatus)
                    {
                        return config;
                    }
                }
            }

            return null;
        }
    }
}