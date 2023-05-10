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
                var roleInfo = zoneScene.GetComponent<RoleInfoComponent>().GetSelfRoleInfo();
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
        
        public static async ETTask<int> SetReadyNetwork(Scene zoneScene,long unitId, int ready)
        {
            LandRoomComponent landRoomComponent = zoneScene.CurrentScene().GetComponent<LandRoomComponent>();
            Session session = zoneScene.GetComponent<SessionComponent>().Session;
            Lo2C_UnitReady lo2CUnitReady;
            try
            {
                landRoomComponent.InReady = true;
                lo2CUnitReady = (Lo2C_UnitReady)await session.Call(new C2Lo_UnitReady() { UnitId = unitId , Ready = ready});
                if (lo2CUnitReady.Error != ErrorCode.ERR_Success)
                {
                    landRoomComponent.InReady = false;
                    Log.Error(lo2CUnitReady.Error.ToString());
                    return lo2CUnitReady.Error;
                }
            }
            catch (Exception e)
            {
                landRoomComponent.InReady = false;
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            landRoomComponent.InReady = false;
            landRoomComponent.SetReady(unitId, ready);
            landRoomComponent.SelfIsReady = ready == 1;

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> RobLandLord(Scene zoneScene, long unitId, int grab)
        {
            Session session = zoneScene.GetComponent<SessionComponent>().Session;
            Lo2C_RobLandLordResponse lo2CRobLandLordResponse;
            try
            {
                lo2CRobLandLordResponse = (Lo2C_RobLandLordResponse)await session.Call(new C2Lo_RobLandLordRequest() { UnitId = unitId, Rob = grab });
                if (lo2CRobLandLordResponse.Error != ErrorCode.ERR_Success)
                {
                    Log.Error(lo2CRobLandLordResponse.Error.ToString());
                    return lo2CRobLandLordResponse.Error;
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            return ErrorCode.ERR_Success;
        }
    }
}