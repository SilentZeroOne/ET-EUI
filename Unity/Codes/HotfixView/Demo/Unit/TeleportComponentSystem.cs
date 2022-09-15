using UnityEngine;

namespace ET
{
    public class TeleportComponentAwakeSystem: AwakeSystem<TeleportComponent>
    {
        public override void Awake(TeleportComponent self)
        {
            var go = self.GetParent<Unit>().GetComponent<GameObjectComponent>().GameObject;
            self.Trigger = go.GetComponent<TriggerAction>();
            if (self.Trigger == null)
            {
                self.Trigger = go.AddComponent<TriggerAction>();
            }

            self.Trigger.OnTriggerEnter2DAction += self.Teleport;
        }
    }

    public class TeleportComponentDestroySystem: DestroySystem<TeleportComponent>
    {
        public override void Destroy(TeleportComponent self)
        {

        }
    }

    [FriendClass(typeof(TeleportComponent))]
    [FriendClassAttribute(typeof(ET.BoundComponent))]
    public static class TeleportComponentSystem
    {
        public static void Teleport(this TeleportComponent self, Collider2D collider)
        {
            if (collider.CompareTag(TagManager.TpPoint))
            {
                var config = TeleportConfigCategory.Instance.GetConfigBySceneNameAndPointName(self.ZoneScene().CurrentScene().Name, collider.name);

                SceneChangeHelper.SceneChangeTo(self.ZoneScene(), config.TargetSceneName, IdGenerater.Instance.GenerateInstanceId(), config.TargetPosX,
                    config.TargetPosY, config.TargetPosZ).Coroutine();
                
                self.WaitEndAndSetCameraBound().Coroutine();
            }
        }

        private static async ETTask WaitEndAndSetCameraBound(this TeleportComponent self)
        {
            await self.ZoneScene().GetComponent<ObjectWait>().Wait<WaitType.Wait_TeleportEnd>();
            var cinemachineComponent = self.Parent.GetComponent<CinemachineComponent>();

            cinemachineComponent.SetConfinerBounding(self.ZoneScene().CurrentScene().GetComponent<BoundComponent>().Bounds);
        }
    }
}