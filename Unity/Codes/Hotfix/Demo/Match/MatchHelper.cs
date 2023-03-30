using System;

namespace ET
{
    public static class MatchHelper
    {
        public static async ETTask<int> StartMatch(Scene zoneScene)
        {
            Lo2C_StartMatch lo2CStartMatch = null;
            try
            {
                var roleInfo = zoneScene.GetComponent<RoleInfoComponent>().RoleInfo;
                lo2CStartMatch = (Lo2C_StartMatch)await zoneScene.GetComponent<SessionComponent>().Session
                        .Call(new C2Lo_StartMatch() { RoleInfoId = roleInfo.Id });
                
                if (lo2CStartMatch.Error != ErrorCode.ERR_Success)
                {
                    Log.Error(lo2CStartMatch.Error.ToString());
                    return lo2CStartMatch.Error;
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }
            
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
    }
}