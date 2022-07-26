namespace ET
{
    [FriendClass(typeof(RankInfo))]
    [FriendClass(typeof(RoleInfo))]
    public static class RankHelper
    {
        public static void AddOrUpdateLevelRank(Unit unit)
        {
            using (RankInfo rankInfo = unit.DomainScene().AddChild<RankInfo>())
            {
                rankInfo.UnitId = unit.Id;
                rankInfo.Name = unit.GetComponent<RoleInfo>().Name;
                rankInfo.Count = unit.GetComponent<NumericComponent>().GetAsInt(NumericType.Level);

                Map2Rank_AddOrUpdateRankInfo map2RankAddOrUpdateRankInfo = new Map2Rank_AddOrUpdateRankInfo();
                map2RankAddOrUpdateRankInfo.RankInfo = rankInfo;

                long instanceId = StartSceneConfigCategory.Instance.GetBySceneName(unit.DomainZone(), "Rank").InstanceId;

                MessageHelper.SendActor(instanceId, map2RankAddOrUpdateRankInfo);
            }
        }
    }
}