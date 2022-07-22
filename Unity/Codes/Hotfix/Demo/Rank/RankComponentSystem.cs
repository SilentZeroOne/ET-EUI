namespace ET
{
    public class RankComponentAwakeSystem: AwakeSystem<RankComponent>
    {
        public override void Awake(RankComponent self)
        {

        }
    }

    public class RankComponentDestroySystem: DestroySystem<RankComponent>
    {
        public override void Destroy(RankComponent self)
        {

        }
    }
    

    [FriendClass(typeof (RankComponent))]
    public static class RankComponentSystem
    {
        public static int GetRankCount(this RankComponent self)
        {
            return self.RankInfos.Count;
        }

        public static void ClearAll(this RankComponent self)
        {
            for (int i = 0; i < self.RankInfos.Count; i++)
            {
                self.RankInfos[i]?.Dispose();
            }

            self.RankInfos.Clear();
        }

        public static RankInfo GetRankInfoByIndex(this RankComponent self, int index)
        {
            if (index < 0 || index > self.RankInfos.Count)
            {
                return null;
            }

            return self.RankInfos[index];
        }
    }
}