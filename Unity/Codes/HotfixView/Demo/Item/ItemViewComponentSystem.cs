﻿using UnityEngine;

namespace ET
{
    public class ItemViewComponentAwakeSystem: AwakeSystem<ItemViewComponent>
    {
        public override void Awake(ItemViewComponent self)
        {
            //self.Init().Coroutine();
        }
    }

    public class ItemViewComponentDestroySystem: DestroySystem<ItemViewComponent>
    {
        public override void Destroy(ItemViewComponent self)
        {

        }
    }

    [FriendClass(typeof (ItemViewComponent))]
    public static class ItemViewComponentSystem
    {
        /// <summary>
        /// 每次都需要手动调用，方便await
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask Init(this ItemViewComponent self)
        {
            Item item = self.GetParent<Item>();
            var go = item.GetComponent<GameObjectComponent>().GameObject;
            go.AddComponent<MonoBridge>().BelongToEntityId = item.InstanceId;
            
            self.SpriteRenderer = go.GetComponentFormRC<SpriteRenderer>("Sprite");
            self.BoxCollider2D = go.GetComponent<BoxCollider2D>();

            Sprite sprite = await IconHelper.LoadIconSpriteAsync(string.IsNullOrEmpty(item.Config.ItemOnWorldSprite)?
                    item.Config.ItemIcon : item.Config.ItemOnWorldSprite);
            self.SpriteRenderer.sprite = sprite;

            //修改boxcollider尺寸
            if (self.BoxCollider2D.isTrigger)//不是Trigger说明提前设置好了对应的大小
            {
                Vector2 newSize = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y);
                self.BoxCollider2D.size = newSize;
                self.BoxCollider2D.offset = new Vector2(0, sprite.bounds.center.y);
            }
        }
    }
}