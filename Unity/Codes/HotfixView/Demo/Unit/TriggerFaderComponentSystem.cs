using System;
using DG.Tweening;
using UnityEngine;

namespace ET
{
    public class TriggerFaderComponentAwakeSystem: AwakeSystem<TriggerFaderComponent>
    {
        public override void Awake(TriggerFaderComponent self)
        {
            var go = self.GetParent<Unit>().GetComponent<GameObjectComponent>().GameObject;
            self.Trigger = go.GetComponent<TriggerAction>();
            if (self.Trigger == null)
            {
                self.Trigger = go.AddComponent<TriggerAction>();
            }
            
            self.Trigger.OnTriggerEnter2DAction += self.FadeOut;
            self.Trigger.OnTriggerExit2DAction += self.FadeIn;
        }
    }

    public class TriggerFaderComponentDestroySystem: DestroySystem<TriggerFaderComponent>
    {
        public override void Destroy(TriggerFaderComponent self)
        {

        }
    }

    [FriendClass(typeof (TriggerFaderComponent))]
    public static class TriggerFaderComponentSystem
    {
        /// <summary>
        /// 逐渐半透明
        /// </summary>
        public static void FadeOut(this TriggerFaderComponent self, Collider2D other)
        {
            if (other.CompareTag(TagManager.FadeObject))
            {
                var renderers = other.GetComponentsInChildren<SpriteRenderer>();
                foreach (var spriteRenderer in renderers)
                {
                    spriteRenderer.DOColor(new Color(1, 1, 1, Settings.FadeAlpha), Settings.FadeDuration);
                }
            }
        }
        
        /// <summary>
        /// 逐渐恢复
        /// </summary>
        public static void FadeIn(this TriggerFaderComponent self, Collider2D other)
        {
            if (other.CompareTag(TagManager.FadeObject))
            {
                var renderers = other.GetComponentsInChildren<SpriteRenderer>();
                foreach (var spriteRenderer in renderers)
                {
                    spriteRenderer.DOColor(new Color(1, 1, 1, 1), Settings.FadeDuration);
                }
            }
        }
    }
}