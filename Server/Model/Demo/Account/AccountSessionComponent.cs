using System.Collections.Generic;

namespace ET
{
    [ComponentOf()]
    public class AccountSessionComponent: Entity, IAwake, IDestroy
    {
        public Dictionary<long, long> AccountSessionDictionary = new Dictionary<long, long>();
    }
}