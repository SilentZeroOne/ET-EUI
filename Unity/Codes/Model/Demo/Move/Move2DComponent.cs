using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class Move2DComponent: Entity, IAwake, IDestroy ,IUpdate,IFixedUpdate
    {
        public Stack<MovementStep> Steps { get; set; }

        public float Speed;// m/s
        
        public Action<bool> Callback;

        public Vector3 StartPos;

        public MovementStep PreStep;

        public MovementStep CurrentStep;
        
        // 开启移动协程的时间
        public long BeginTime;

        // 每个点的开始时间
        public long StartTime { get; set; }
        
        public long NeedTime;// ms

        public long MoveTimer;
    }
}