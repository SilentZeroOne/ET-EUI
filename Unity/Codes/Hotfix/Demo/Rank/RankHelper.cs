using System;

namespace ET
{
    public static class RankHelper
    {
        public static async ETTask<int> GetRankInfo(Scene zoneScene)
        {
            Rank2C_GetRankInfos rank2CGetRankInfos = null;
            try
            {
                rank2CGetRankInfos = (Rank2C_GetRankInfos)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2Rank_GetRankInfos());
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            if (rank2CGetRankInfos.Error != ErrorCode.ERR_Success)
            {
                return rank2CGetRankInfos.Error;
            }
            
            zoneScene.GetComponent<RankComponent>().ClearAll();
            for (int i = 0; i < rank2CGetRankInfos.RankInfoProtoList.Count; i++)
            {
                zoneScene.GetComponent<RankComponent>().Add(rank2CGetRankInfos.RankInfoProtoList[i]);
            }

            return rank2CGetRankInfos.Error;
        }
    }
}