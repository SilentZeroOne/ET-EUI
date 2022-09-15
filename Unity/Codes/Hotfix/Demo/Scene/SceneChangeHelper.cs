using UnityEngine;

namespace ET
{
    public static class SceneChangeHelper
    {
        // 场景切换协程
        public static async ETTask SceneChangeTo(Scene zoneScene, string sceneName, long sceneInstanceId)
        {
            //zoneScene.RemoveComponent<AIComponent>();
            
            CurrentScenesComponent currentScenesComponent = zoneScene.GetComponent<CurrentScenesComponent>();
            currentScenesComponent.Scene?.Dispose(); // 删除之前的CurrentScene，创建新的
            Scene currentScene = SceneFactory.CreateCurrentScene(sceneInstanceId, zoneScene.Zone, sceneName, currentScenesComponent);
            UnitComponent unitComponent = currentScene.AddComponent<UnitComponent>();

            currentScenesComponent.HaveCache = await currentScene.GetComponent<ItemsComponent>().LoadItemsComponent();
            
            Game.EventSystem.Publish(new EventType.SceneChangeStart() {ZoneScene = zoneScene});

            // 等待CreateMyUnit的消息
            // WaitType.Wait_CreateMyUnit waitCreateMyUnit = await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_CreateMyUnit>();
            // M2C_CreateMyUnit m2CCreateMyUnit = waitCreateMyUnit.Message;

            await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_SceneLoaded>();

            Unit unit = UnitFactory.Create(currentScene);
            //unitComponent.Add(unit);

            if (!currentScenesComponent.HaveCache)
            {
                ItemFactory.Create(currentScene, 1010);
                ItemFactory.Create(currentScene, 1008);
                ItemFactory.Create(currentScene, 1002);
                ItemFactory.Create(currentScene, 1005);
            }

            Game.EventSystem.PublishAsync(new EventType.SceneChangeFinish() {ZoneScene = zoneScene, CurrentScene = currentScene}).Coroutine();

            // 通知等待场景切换的协程
            zoneScene.GetComponent<ObjectWait>().Notify(new WaitType.Wait_SceneChangeFinish());
        }

        /// <summary>
        /// Used for teleport
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <param name="sceneName"></param>
        /// <param name="sceneInstanceId"></param>
        /// <param name="targetPosX"></param>
        /// <param name="targetPosY"></param>
        /// <param name="targetPosZ"></param>
        public static async ETTask SceneChangeTo(Scene zoneScene, string sceneName, long sceneInstanceId, float targetPosX, float targetPosY,
        float targetPosZ)
        {
            CurrentScenesComponent currentScenesComponent = zoneScene.GetComponent<CurrentScenesComponent>();
            //获得之前的Unit  加入新的CurrentScene
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(currentScenesComponent.Scene);
            var previousCurrentScene = currentScenesComponent.Scene;
            var previousName = previousCurrentScene.Name;

            Scene currentScene = SceneFactory.CreateCurrentScene(sceneInstanceId, zoneScene.Zone, sceneName, currentScenesComponent);
            
            //Read items cache
            currentScenesComponent.HaveCache = await currentScene.GetComponent<ItemsComponent>().LoadItemsComponent();
            
            UnitComponent unitComponent = currentScene.AddComponent<UnitComponent>();
            unitComponent.AddChild(unit);

            previousCurrentScene?.Dispose();// 删除之前的CurrentScene
            
            Game.EventSystem.Publish(new EventType.SceneChangeStart() { ZoneScene = zoneScene, PreviousSceneName = previousName });
            
            await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_SceneLoaded>();
            
            //Move unit to target position
            unit.Position = new Vector3(targetPosX, targetPosY, targetPosZ);
            
            //Test loading scene with long time
            await TimerComponent.Instance.WaitAsync(2000);
            
            Game.EventSystem.PublishAsync(new EventType.SceneChangeFinish() {ZoneScene = zoneScene, CurrentScene = currentScene}).Coroutine();
            zoneScene.GetComponent<ObjectWait>().Notify(new WaitType.Wait_TeleportEnd());
        }
    }
}