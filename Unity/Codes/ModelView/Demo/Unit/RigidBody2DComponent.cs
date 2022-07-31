using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class RigidBody2DComponent: Entity, IAwake, IDestroy
    {
        public Rigidbody2D Rigidbody2D;
    }
}