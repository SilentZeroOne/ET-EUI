using System;
using System.Collections.Generic;

namespace ET
{
    public static class RobotCaseSystem
    {
        // 创建机器人，生命周期是RobotCase
        public static async ETTask NewRobot(this RobotCase self, int count, List<Scene> scenes)
        {
            ETTask[] tasks = new ETTask[count];
            for (int i = 0; i < count; ++i)
            {
                tasks[i] = self.NewRobot(scenes);
            }

            await ETTaskHelper.WaitAll(tasks);
        }

        private static async ETTask NewRobot(this RobotCase self, List<Scene> scenes)
        {
            try
            {
                scenes.Add(await self.NewRobot());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        // 创建机器人，生命周期是RobotCase
        public static async ETTask NewZoneRobot(this RobotCase self, int zone, int count, List<Scene> scenes)
        {
            ETTask[] tasks = new ETTask[count];
            for (int i = 0; i < count; ++i)
            {
                tasks[i] = self.NewZoneRobot(zone + i, scenes);
            }

            await ETTaskHelper.WaitAll(tasks);
        }

        // 这个方法创建的是进程所属的机器人，建议使用RobotCase.NewRobot来创建
        private static async ETTask NewZoneRobot(this RobotCase self, int zone, List<Scene> scenes)
        {
            try
            {
                scenes.Add(await self.NewRobot(zone));
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETTask<Scene> NewRobot(this RobotCase self, int zone)
        {
            return await self.NewRobot(zone, $"Robot_{zone}");
        }

        public static async ETTask<Scene> NewRobot(this RobotCase self, int zone, string name)
        {
            Scene zoneScene = null;
            try
            {
                zoneScene = SceneFactory.CreateZoneScene(zone, name, self);
                await LoginHelper.Register(zoneScene, ConstValue.LoginAddress, $"Robot{zone}", $"Robot{zone}");
                await LoginHelper.Login(zoneScene, ConstValue.LoginAddress, $"Robot{zone}", $"Robot{zone}");
                if (zoneScene.GetComponent<RoleInfoComponent>().GetSelfRoleInfo() == null)
                {
                    await LoginHelper.CreateRole(zoneScene, $"Robot{zone}");
                }
                
                //await EnterMapHelper.EnterMapAsync(zoneScene);
                
                await LoginHelper.GetRealm(zoneScene);
                await LoginHelper.EnterGame(zoneScene);

                await MatchHelper.StartMatch(zoneScene);
                Log.Debug($"create robot ok: {zone}");
                return zoneScene;
            }
            catch (Exception e)
            {
                zoneScene?.Dispose();
                throw new Exception($"RobotCase create robot fail, zone: {zone}", e);
            }
        }

        private static async ETTask<Scene> NewRobot(this RobotCase self)
        {
            int zone = self.GetParent<RobotCaseComponent>().GetN();
            Scene zoneScene = null;

            try
            {
                zoneScene = SceneFactory.CreateZoneScene(zone, $"Robot_{zone}", self);
                await LoginHelper.Register(zoneScene, ConstValue.LoginAddress, $"Robot{zone}", $"Robot{zone}");
                await LoginHelper.Login(zoneScene, ConstValue.LoginAddress, $"Robot{zone}", $"Robot{zone}");
                if (zoneScene.GetComponent<RoleInfoComponent>().GetSelfRoleInfo() == null)
                {
                    await LoginHelper.CreateRole(zoneScene, $"Robot{zone}");
                }

                await LoginHelper.GetRealm(zoneScene);
                await LoginHelper.EnterGame(zoneScene);

                await MatchHelper.StartMatch(zoneScene);
                Log.Debug($"create robot ok: {zone}");
                return zoneScene;
            }
            catch (Exception e)
            {
                zoneScene?.Dispose();
                throw new Exception($"RobotCase create robot fail, zone: {zone}", e);
            }
        }
    }
}