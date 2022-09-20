namespace ET
{
    public class CropAwakeSystem: AwakeSystem<Crop,int>
    {
        public override void Awake(Crop self,int configId)
        {
            self.ConfigId = configId;
        }
    }

    public class CropDestroySystem: DestroySystem<Crop>
    {
        public override void Destroy(Crop self)
        {

        }
    }

    [FriendClass(typeof (Crop))]
    public static class CropSystem
    {
        public static void Test(this Crop self)
        {
        }

        public static void FromProto(this Crop self, CropInfo proto)
        {
            self.ConfigId = proto.ConfigId;
            self.LastStage = proto.LastStage;
        }
        
        public static CropInfo ToProto(this Crop self)
        {
            if (self == null) return null;
            
            return new CropInfo()
            {
                CropId = self.Id,
                ConfigId = self.ConfigId,
                LastStage = self.LastStage
            };
        }
    }
}