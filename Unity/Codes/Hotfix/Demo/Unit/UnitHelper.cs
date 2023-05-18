using System;

namespace ET
{
    public static class UnitHelper
    {
        public static Unit GetMyUnitFromZoneScene(Scene zoneScene)
        {
            PlayerComponent playerComponent = zoneScene.GetComponent<PlayerComponent>();
            Scene currentScene = zoneScene.GetComponent<CurrentScenesComponent>().Scene;
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }

        public static Unit GetMyUnitFromCurrentScene(Scene currentScene)
        {
            PlayerComponent playerComponent = currentScene.Parent.Parent.GetComponent<PlayerComponent>();
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }

        public static Unit GetUnit(Scene zoneScene, long unitId)
        {
            Scene currentScene = zoneScene.GetComponent<CurrentScenesComponent>().Scene;
            return currentScene.GetComponent<UnitComponent>().Get(unitId);
        }

        public static async ETTask<RoleInfo> GetRoleInfo(Scene zoneScene, long unitId)
        {
            G2C_GetRoleInfo g2CGetRoleInfo = null;
            try
            {
                g2CGetRoleInfo = (G2C_GetRoleInfo)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_GetRoleInfo()
                {
                    UnitId = unitId
                });

            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return null;
            }

            if (g2CGetRoleInfo.Error != ErrorCode.ERR_Success)
            {
                Log.Error(g2CGetRoleInfo.Error.ToString());
                return null;
            }

            var roleInfo = zoneScene.GetComponent<RoleInfoComponent>().AddChildWithId<RoleInfo>(g2CGetRoleInfo.RoleInfo.Id);
            roleInfo.FromMessage(g2CGetRoleInfo.RoleInfo);

            return roleInfo;
        }

        public static RoleInfo GetCachedRoleInfo(Scene zoneScene, long unitId)
        {
            return zoneScene.GetComponent<RoleInfoComponent>().GetChild<RoleInfo>(unitId);
        }

        public static int GetSeatIndex(this Unit unit)
        {
            var myUnit = GetMyUnitFromZoneScene(unit.ZoneScene());
            
            if (unit.SeatIndex == (myUnit.SeatIndex + 1) % 3)
            {
                return 1;
            }
            
            return 2;
        }
    }
}