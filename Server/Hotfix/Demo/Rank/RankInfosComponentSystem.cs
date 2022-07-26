namespace ET
{
    public class RankInfosComponentAwakeSystem: AwakeSystem<RankInfosComponent>
    {
        public override void Awake(RankInfosComponent self)
        {

        }
    }

    public class RankInfosComponentDestroySystem: DestroySystem<RankInfosComponent>
    {
        public override void Destroy(RankInfosComponent self)
        {

        }
    }
    

    [FriendClass(typeof (RankInfosComponent))]
    [FriendClass(typeof(RankInfo))]
    public static class RankInfosComponentSystem
    {
        public static void Add(this RankInfosComponent self, RankInfo rankInfo)
        {
            self.AddChild(rankInfo);
            self.RankInfosDict.Add(rankInfo.UnitId, rankInfo);
            self.SortedRankInfoList.Add(rankInfo, rankInfo.UnitId);
        }
        
        public static async ETTask LoadRankInfo(this RankInfosComponent self)
        {
            var rankInfoList = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                    .Query<RankInfo>(d => true, collection: "RankInfosComponent");
            foreach (var rankInfo in rankInfoList)
            {
                self.Add(rankInfo);
            }
        }

        public static void AddOrUpdate(this RankInfosComponent self, RankInfo newRankInfo)
        {
            if (self.RankInfosDict.ContainsKey(newRankInfo.UnitId))
            {
                var oldRankInfo = self.RankInfosDict[newRankInfo.UnitId];
                if (oldRankInfo.Count == newRankInfo.Count)
                {
                    return;
                }
                
                DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Remove<RankInfo>(oldRankInfo.UnitId,oldRankInfo.Id,"RankInfosComponent").Coroutine();
                self.RankInfosDict.Remove(oldRankInfo.UnitId);
                self.SortedRankInfoList.Remove(oldRankInfo);
                oldRankInfo?.Dispose();
            }

            self.Add(newRankInfo);
            DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(newRankInfo.UnitId, newRankInfo, "RankInfosComponent").Coroutine();
        }

        
    }
}