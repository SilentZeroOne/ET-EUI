namespace ET
{
    public class UnitCacheComponentAwakeSystem : AwakeSystem<UnitCacheComponent>
    {
        public override void Awake(UnitCacheComponent self)
        {
            self.UnitCachesKeyList.Clear();
            foreach (var type in Game.EventSystem.GetTypes().Values)
            {
                if (type != typeof (IUnitCache) && typeof (IUnitCache).IsAssignableFrom(type))
                {
                    self.UnitCachesKeyList.Add(type.Name);
                }
            }

            foreach (var key in self.UnitCachesKeyList)
            {
                UnitCache unitCache = self.AddChild<UnitCache>();
                unitCache.Key = key;
                self.UnitCaches.Add(key, unitCache);
            }
        }
    }

    public class UnitCacheComponentDestroySystem: DestroySystem<UnitCacheComponent>
    {
        public override void Destroy(UnitCacheComponent self)
        {
            foreach (var caches in self.UnitCaches.Values)
            {
                caches?.Dispose();
            }
            self.UnitCaches.Clear();
        }
    }

    public static class UnitCacheComponentSystem
    {
        public static async ETTask AddOrUpdate(this UnitCacheComponent self, long id, ListComponent<Entity> entities)
        {
            using (ListComponent<Entity> list = ListComponent<Entity>.Create())
            {
                foreach (var entity in entities)
                {
                    string key = entity.GetType().Name;
                    if (!self.UnitCaches.TryGetValue(key, out var unitCache))
                    {
                        unitCache = self.AddChild<UnitCache>();
                        unitCache.Key = key;
                        self.UnitCaches.Add(key, unitCache);
                    }
                    unitCache.AddOrUpdate(entity);
                    list.Add(entity);
                }

                if (list.Count > 0)
                {
                    await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(id, list);
                }
            }
        }

        public static async ETTask<Entity> Get(this UnitCacheComponent self, long id, string key)
        {
            if (!self.UnitCaches.TryGetValue(key, out var unitCache))
            {
                unitCache = self.AddChild<UnitCache>();
                unitCache.Key = key;
                self.UnitCaches.Add(key, unitCache);
            }

            return await unitCache.Get(id);
        }

        public static void Delete(this UnitCacheComponent self, long unitId)
        {
            foreach (var cache in self.UnitCaches.Values)
            {
                cache.Delete(unitId);
            }
        }
    }
}