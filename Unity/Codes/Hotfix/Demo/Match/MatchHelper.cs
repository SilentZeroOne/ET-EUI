using System;

namespace ET
{
    public static class MatchHelper
    {
        public static async ETTask<int> StartMatch(Scene zoneScene)
        {
            M2C_StartMatch m2CStartMatch = null;
            try
            {
                var roleInfo = zoneScene.GetComponent<RoleInfoComponent>().RoleInfo;
                m2CStartMatch = (M2C_StartMatch)await zoneScene.GetComponent<SessionComponent>().Session
                        .Call(new C2M_StartMatch() { RoleInfoId = roleInfo.Id });
                
                if (m2CStartMatch.Error != ErrorCode.ERR_Success)
                {
                    Log.Error(m2CStartMatch.Error.ToString());
                    return m2CStartMatch.Error;
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