using System.Collections.Generic;

namespace ET
{
    public interface IUnitCache
    {
        
    }
    
    public class UnitCache : Entity,IAwake,IDestroy
    {
        public string Key;
        
        public Dictionary<long, Entity> CacheComponentsDict = new Dictionary<long, Entity>();
    }
}