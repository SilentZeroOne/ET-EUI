using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class BoundComponent: Entity, IAwake, IDestroy
    {
        public PolygonCollider2D Bounds;
    }
}