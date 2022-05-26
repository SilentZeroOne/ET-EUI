namespace ET
{
    public class UnitCacheDestroySystem : DestroySystem<UnitCache>
    {
        public override void Destroy(UnitCache self)
        {
            foreach (var entity in self.CacheComponentsDict.Values)
            {
                entity.Dispose();
            }
            self.CacheComponentsDict.Clear();
            self.Key = null;
        }
    }

    [FriendClass(typeof(UnitCache))]
    public static class UnitCacheSystem
    {
        public static void AddOrUpdate(this UnitCache self, Entity entity)
        {
            if (entity == null || entity.IsDisposed)
            {
                return;
            }

            if (self.CacheComponentsDict.TryGetValue(entity.Id, out var oldEntity))
            {
                if (entity != oldEntity)
                {
                    oldEntity.Dispose();
                }

                self.CacheComponentsDict.Remove(entity.Id);
            }
            
            self.CacheComponentsDict.Add(entity.Id,entity);
        }

        public static async ETTask<Entity> Get(this UnitCache self, long unitId)
        {
            if (!self.CacheComponentsDict.TryGetValue(unitId, out var entity))
            {
                entity = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<Entity>(unitId, self.Key);
                if (entity != null)
                {
                    self.AddOrUpdate(entity);
                }
            }

            return entity;
        }

        public static void Delete(this UnitCache self, long unitId)
        {
            if (self.CacheComponentsDict.TryGetValue(unitId, out var entity))
            {
                entity.Dispose();
                self.CacheComponentsDict.Remove(unitId);
            }
        }
    }
}