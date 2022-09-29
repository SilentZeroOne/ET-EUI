using System;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class TriggerComponent: Entity, IAwake, IDestroy
    {
        public TriggerAction Trigger;
    }
}