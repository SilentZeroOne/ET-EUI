using System;
using DG.Tweening;
using UnityEngine;

namespace ET
{
    public class TriggerComponentAwakeSystem: AwakeSystem<TriggerComponent>
    {
        public override void Awake(TriggerComponent self)
        {
            var go = self.GetParent<Unit>().GetComponent<GameObjectComponent>().GameObject;
            self.Trigger = go.GetComponent<TriggerAction>();
            if (self.Trigger == null)
            {
                self.Trigger = go.AddComponent<TriggerAction>();
            }
            
            self.Trigger.OnTriggerEnter2DAction += self.OnTriggerEnter;
            self.Trigger.OnTriggerExit2DAction += self.OnTriggerExit;
        }
    }

    public class TriggerComponentDestroySystem: DestroySystem<TriggerComponent>
    {
        public override void Destroy(TriggerComponent self)
        {

        }
    }

    [FriendClass(typeof(TriggerComponent))]
    [FriendClassAttribute(typeof(ET.BoundComponent))]
    public static class TriggerFaderComponentSystem
    {
        public static void OnTriggerEnter(this TriggerComponent self, Collider2D other)
        {
            Unit unit = self.GetParent<Unit>();
            if (other.CompareTag(TagManager.FadeObject))
            {
                // 逐渐半透明
                var renderers = other.GetComponentsInChildren<SpriteRenderer>();
                foreach (var spriteRenderer in renderers)
                {
                    spriteRenderer.DOColor(new Color(1, 1, 1, Settings.FadeAlpha), Settings.FadeDuration);
                }
            }

            if (other.CompareTag(TagManager.TpPoint))
            {
                var config = TeleportConfigCategory.Instance.GetConfigBySceneNameAndPointName(self.ZoneScene().CurrentScene().Name, other.name);

                switch ((UnitType)unit.Config.Type)
                {
                    case UnitType.Player:
                        SceneChangeHelper.SceneChangeTo(self.ZoneScene(), config.TargetSceneName, IdGenerater.Instance.GenerateInstanceId(), config.TargetPosX,
                            config.TargetPosY, config.TargetPosZ).Coroutine();

                        self.WaitEndAndSetCameraBound().Coroutine();
                        break;
                    case UnitType.Monster:
                        break;
                    case UnitType.NPC:
                        
                        break;
                }
            }

            var monoBridge = other.GetComponent<MonoBridge>();
            if (monoBridge)
            {
                Entity entity = Game.EventSystem.Get(monoBridge.BelongToEntityId);
                if (entity is Crop crop)
                {
                    self.GrassDoAnimation(crop, other.gameObject.transform).Coroutine();
                }
            }
        }

        public static void OnTriggerExit(this TriggerComponent self, Collider2D other)
        {
            if (other.CompareTag(TagManager.FadeObject))
            {
                // 逐渐恢复
                var renderers = other.GetComponentsInChildren<SpriteRenderer>();
                foreach (var spriteRenderer in renderers)
                {
                    spriteRenderer.DOColor(new Color(1, 1, 1, 1), Settings.FadeDuration);
                }
            }

            var monoBridge = other.GetComponent<MonoBridge>();
            if (monoBridge)
            {
                Entity entity = Game.EventSystem.Get(monoBridge.BelongToEntityId);
                if (entity is Crop crop)
                {
                    self.GrassDoAnimation(crop, other.gameObject.transform).Coroutine();
                }
            }
        }

        private static async ETTask GrassDoAnimation(this TriggerComponent self, Crop crop, Transform other)
        {
            if (crop.CanHarvest(1004).Item1 && !crop.IsPlayingAnimation) //能被镰刀收割，说明是杂草
            {
                var player = self.GetParent<Unit>().GetComponent<GameObjectComponent>().GameObject.transform;

                crop.IsPlayingAnimation = true;
                int dotV = player.position.x < other.position.x ? 1 : -1;

                other.DORotate(new Vector3(0, 0, 8 * dotV), 0.16f);
                await TimerComponent.Instance.WaitAsync(160);
                other.DORotate(new Vector3(0, 0, 10 * -dotV), 0.2f);
                await TimerComponent.Instance.WaitAsync(200);
                other.DORotate(new Vector3(0, 0, 2 * dotV), 0.04f);

                crop.IsPlayingAnimation = false;
            }
        }

        private static async ETTask WaitEndAndSetCameraBound(this TriggerComponent self)
        {
            await self.ZoneScene().GetComponent<ObjectWait>().Wait<WaitType.Wait_TeleportEnd>();
            var cinemachineComponent = self.Parent.GetComponent<CinemachineComponent>();

            cinemachineComponent.SetConfinerBounding(self.ZoneScene().CurrentScene().GetComponent<BoundComponent>().Bounds);
        }
    }
}