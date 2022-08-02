using Cinemachine;
using UnityEngine;

namespace ET
{
    public class CinemachineComponentAwakeSystem: AwakeSystem<CinemachineComponent, GameObject>
    {
        public override void Awake(CinemachineComponent self, GameObject a)
        {
            self.AddComponent<GameObjectComponent>().GameObject = a;
            self.Confiner = a.GetComponent<CinemachineConfiner>();
        }
    }

    public class CinemachineComponentDestroySystem: DestroySystem<CinemachineComponent>
    {
        public override void Destroy(CinemachineComponent self)
        {

        }
    }

    [FriendClass(typeof (CinemachineComponent))]
    public static class CinemachineComponentSystem
    {
        public static void SetConfinerBounding(this CinemachineComponent self,Collider2D collider)
        {
            self.Confiner.m_BoundingShape2D = collider;
            self.Confiner.InvalidatePathCache();
        }
    }
}