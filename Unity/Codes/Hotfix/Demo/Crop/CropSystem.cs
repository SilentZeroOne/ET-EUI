using System;
using ET.EventType;

namespace ET
{
    public class CropAwakeSystem: AwakeSystem<Crop,int>
    {
        public override void Awake(Crop self,int configId)
        {
            self.ConfigId = configId;
            self.LastStage = -1;
        }
    }

    public class CropDestroySystem: DestroySystem<Crop>
    {
        public override void Destroy(Crop self)
        {

        }
    }

    [FriendClass(typeof(Crop))]
    [FriendClassAttribute(typeof(ET.Item))]
    public static class CropSystem
    {
        public static void ProcessToolAction(this Crop self, Item tool)
        {
            var value = self.CanHarvest(tool.ConfigId);
            if (value.Item1)
            {
                if (self.HarvestActionCount < self.Config.RequireActionCount[value.Item2])
                {
                    self.HarvestActionCount++;
                    
                    //判断是否有动画  树木等
                }

                if (self.HarvestActionCount >= self.Config.RequireActionCount[value.Item2])
                {
                    if (self.Config.GenerateAtPlayerPos == 1)
                    {
                        Game.EventSystem.Publish(new HarvestCrop() { Crop = self, ZoneScene = self.ZoneScene() });
                    }
                }
            }
        }

        public static void FromProto(this Crop self, CropInfo proto)
        {
            self.ConfigId = proto.ConfigId;
            self.LastStage = proto.LastStage;
            self.HarvestActionCount = proto.HarvestActionCount;
        }

        public static CropInfo ToProto(this Crop self)
        {
            if (self == null) return null;

            return new CropInfo()
            {
                CropId = self.Id,
                ConfigId = self.ConfigId,
                LastStage = self.LastStage,
                HarvestActionCount = self.HarvestActionCount,
            };
        }

        /// <summary>
        /// bool返回是否能收割，int返回可收割工具的index
        /// </summary>
        /// <param name="self"></param>
        /// <param name="toolId"></param>
        /// <returns></returns>
        public static (bool,int) CanHarvest(this Crop self, int toolId)
        {
            var count = self.Config.HarvestToolItemIDs.Length;
            for (int i = 0; i < count; i++)
            {
                if (self.Config.HarvestToolItemIDs[i] == toolId)
                {
                    return (true, i);
                }
            }

            return (false, -1);
        }
    }
}