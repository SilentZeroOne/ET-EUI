using System;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Item))]
    public class ItemBounceComponent: Entity, IAwake<Vector3,Vector3>, IDestroy
    {
        public SpriteRenderer ShadowRenderer;

        public Vector3 TargetPos;

        public Vector3 StartPos;
        
        public Vector2 Direction;

        public float Distance;

        public Transform ItemTransform;

        public Action CallBack;

        public long Timer;

        public bool IsGround;
    }
}