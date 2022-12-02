namespace ET
{
    [FriendClassAttribute(typeof(ET.UnitCache))]
    public class UnitCacheComponentAwakeSystem : AwakeSystem<UnitCacheComponent>
    {
        public override void Awake(UnitCacheComponent self)
        {
            self.UnitCacheKeyList.Clear();
            foreach (var type in Game.EventSystem.GetTypes().Values)
            {
                if (type != typeof(IUnitCache) && typeof(IUnitCache).IsAssignableFrom(type))
                {
                    self.UnitCacheKeyList.Add(type.Name);
                }
            }

            foreach (var key in self.UnitCacheKeyList)
            {
                UnitCache unitCache = self.AddChild<UnitCache>();
                unitCache.key = key;
                self.UnitCaches.Add(key, unitCache);
            }
        }
    }

    public class UnitCacheComponentDestroySystem: DestroySystem<UnitCacheComponent>
    {
        public override void Destroy(UnitCacheComponent self)
        {
            foreach (var unitCache in self.UnitCaches.Values)
            {
                unitCache?.Dispose();
            }
            
            self.UnitCaches.Clear();
        }
    }

    [FriendClass(typeof(UnitCacheComponent))]
    [FriendClassAttribute(typeof(ET.UnitCache))]
    public static class UnitCacheComponentSystem
    {
        private static UnitCache GetUnitCache(this UnitCacheComponent self, string key)
        {
            if (!self.UnitCaches.TryGetValue(key, out var unitCache))
            {
                unitCache = self.AddChild<UnitCache>();
                unitCache.key = key;
                self.UnitCaches.Add(key, unitCache);
            }

            return unitCache;
        }
        
        public static async ETTask<Entity> Get(this UnitCacheComponent self, long unitId, string key)
        {
            var unitCache = self.GetUnitCache(key);

            return await unitCache.Get(unitId);
        }

        public static async ETTask<T> Get<T>(this UnitCacheComponent self, long unitId) where T : Entity
        {
            string key = typeof (T).Name;

            var unitCache = self.GetUnitCache(key);

            return await unitCache.Get(unitId) as T;
        }

        public static async ETTask AddOrUpdate(this UnitCacheComponent self, long id, ListComponent<Entity> entityList)
        {
            using ListComponent<Entity> list = ListComponent<Entity>.Create();
            foreach (var entity in entityList)
            {
                string key = entity.GetType().Name;
                var unitCache = self.GetUnitCache(key);
                unitCache.AddOrUpdate(entity);
                list.Add(entity);
            }

            //存数据库
            if (list.Count > 0)
            {
                await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(id, list);
            }
        }

        public static void Delete(this UnitCacheComponent self, long unitId)
        {
            foreach (var unitCache in self.UnitCaches.Values)
            {
                unitCache.Delete(unitId);
            }
        }
    }
}