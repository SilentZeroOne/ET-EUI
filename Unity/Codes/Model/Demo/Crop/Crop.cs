using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class Crop: Entity, IAwake ,IAwake<int>, IDestroy
    {
        public int ConfigId;

        [BsonIgnore]
        public CropConfig Config => CropConfigCategory.Instance.Get(this.ConfigId);
        
        public int LastStage { get; set; }
        public int HarvestActionCount { get; set; }
        
        public bool IsPlayingAnimation { get; set; }
    }
}