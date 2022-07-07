namespace ET
{
    public class ItemAwakeSystem : AwakeSystem<Item,int>
    {
        public override void Awake(Item self, int configId)
        {
            self.ConfigId = configId;
        }
    }
    
    public class ItemDestorySystem : DestroySystem<Item>
    {
        public override void Destroy(Item self)
        {
            self.ConfigId = 0;
            self.Quality = 0;
        }
    }

    [FriendClass(typeof(Item))]
    public static class ItemSystem
    {
        public static void RandomQuality(this Item self)
        {
            int rate = RandomHelper.RandomNumber(0, 10000);
            if (rate < 4000)
            {
                self.Quality = (int)ItemQuality.General;
            }
            else if(rate <7000)
            {
                self.Quality = (int)ItemQuality.Good;
            }
            else if(rate <8500)
            {
                self.Quality = (int)ItemQuality.Excellent;
            }
            else if(rate <9500)
            {
                self.Quality = (int)ItemQuality.Epic;
            }
            else if(rate <10000)
            {
                self.Quality = (int)ItemQuality.Legand;
            }
        }

        public static ItemInfo ToMessage(this Item self,bool isAllInfo = true)
        {
            ItemInfo info = new ItemInfo() { ItemConfigId = self.ConfigId, ItemQuality = self.Quality, ItemUid = self.Id };
            if (!isAllInfo)
                return info;
            
            //TODO EquipInfoComponent

            return info;
        }
    }
}