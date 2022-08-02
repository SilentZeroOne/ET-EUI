using System;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class TriggerFaderComponent: Entity, IAwake<GameObject>, IDestroy
    {
        public TriggerAction Trigger;
    }
}