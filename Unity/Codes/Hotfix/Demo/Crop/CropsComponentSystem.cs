namespace ET
{
    public class CropsComponentAwakeSystem: AwakeSystem<CropsComponent>
    {
        public override void Awake(CropsComponent self)
        {

        }
    }

    public class CropsComponentDestroySystem: DestroySystem<CropsComponent>
    {
        public override void Destroy(CropsComponent self)
        {

        }
    }

    [FriendClass(typeof (CropsComponent))]
    public static class CropsComponentSystem
    {
        public static void AddCrop(this CropsComponent self,Crop crop)
        {
            if (!self.CropList.Contains(crop))
            {
                self.CropList.Add(crop);
                if (crop.Parent != self)
                {
                    self.AddChild(crop);
                }
            }
        }
        
        public static Crop AddCrop(this CropsComponent self, int cropId)
        {
            Crop crop = self.AddChild<Crop, int>(cropId);
            self.CropList.Add(crop);
            return crop;
        }

        public static void RemoveCrop(this CropsComponent self, Crop crop)
        {
            if (self.CropList.Contains(crop))
            {
                self.CropList.Remove(crop);
            }
        }
        
        //TODO:存储
        
    }
}