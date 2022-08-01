using UnityEngine;

namespace ET
{
    public class BoundComponentAwakeSystem: AwakeSystem<BoundComponent>
    {
        public override void Awake(BoundComponent self)
        {

        }
    }

    public class BoundComponentDestroySystem: DestroySystem<BoundComponent>
    {
        public override void Destroy(BoundComponent self)
        {

        }
    }

    [FriendClass(typeof (BoundComponent))]
    public static class BoundComponentSystem
    {
        public static void SetBound(this BoundComponent self,PolygonCollider2D bound)
        {
            self.Bounds = bound;
        }
    }
}