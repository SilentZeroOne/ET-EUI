using System;
using System.Collections.Generic;

namespace ET
{
    public class UnitSaveDBComponent : Entity , IAwake,IDestroy
    {
        public HashSet<Type> EntityChangeTypeSet { get; } = new HashSet<Type>();

        public long Timer;
    }
}