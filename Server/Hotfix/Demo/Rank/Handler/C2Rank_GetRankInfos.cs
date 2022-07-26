using System;

namespace ET
{
    [FriendClass(typeof(RankInfosComponent))]
    public class C2Rank_GetRankInfosHandler: AMActorRpcHandler<Scene, C2Rank_GetRankInfos, Rank2C_GetRankInfos>
    {
        protected override async ETTask Run(Scene scene, C2Rank_GetRankInfos request, Rank2C_GetRankInfos response, Action reply)
        {
            RankInfosComponent rankInfosComponent = scene.GetComponent<RankInfosComponent>();
            foreach (var rankInfo in rankInfosComponent.SortedRankInfoList)
            {
                response.RankInfoProtoList.Add(rankInfo.Key.ToMessage());
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}