using System;
using UnityEngine;

namespace ET
{
    public static class GameObjectComponentSystem
    {
        [ObjectSystem]
        public class DestroySystem: DestroySystem<GameObjectComponent>
        {
            public override void Destroy(GameObjectComponent self)
            {
                UnityEngine.Object.Destroy(self.GameObject);
            }
        }

        public static void SetGameObject(this GameObjectComponent self, GameObject gameObject)
        {
            gameObject.AddComponent<EntityMonoBridge>().BelongToEntity = self.GetParent<Entity>().InstanceId;
            self.GameObject = gameObject;
        }
    }
}