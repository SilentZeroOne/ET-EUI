using System.Collections.Generic;

namespace ET
{
    [ComponentOf()]
    public class TokenComponent: Entity, IAwake, IDestroy
    {
        /// <summary>
        /// Account Id -> Token
        /// </summary>
        public Dictionary<long, string> TokenDictionary = new Dictionary<long, string>();
    }
}