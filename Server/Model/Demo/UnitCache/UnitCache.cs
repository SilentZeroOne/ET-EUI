using System.Collections.Generic;

namespace ET
{
    //继承这个接口的Entity可以存入数据库
    public interface IUnitCache
    {
        
    }
    
    public class UnitCache: Entity, IAwake, IDestroy
    {
        public string key;

        //Component Id 缓存的component字典
        public Dictionary<long, Entity> CacheComponentsDictionary = new Dictionary<long, Entity>();
    }
}