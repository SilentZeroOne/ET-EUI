namespace ET
{
    public class UnitCacheDestroySystem: DestroySystem<UnitCache>
    {
        public override void Destroy(UnitCache self)
        {
            foreach (var component in self.CacheComponentsDictionary.Values)
            {
                component?.Dispose();
            }

            self.key = null;
        }
    }

    [FriendClass(typeof (UnitCache))]
    public static class UnitCacheSystem
    {
        
        public static void AddOrUpdate(this UnitCache self,Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (self.CacheComponentsDictionary.TryGetValue(entity.Id, out var oldEntity))
            {
                if (entity != oldEntity)
                {
                    oldEntity.Dispose();
                }

                self.CacheComponentsDictionary.Remove(entity.Id);
            }

            self.CacheComponentsDictionary.Add(entity.Id, entity);
        }
        
        /// <summary>
        /// 先从缓存中寻找，找不到去数据库查询 然后存入缓存服
        /// </summary>
        /// <param name="self"></param>
        /// <param name="unitId">Component id</param>
        public static async ETTask<Entity> Get(this UnitCache self, long unitId)
        {
            Entity entity = null;
            if (!self.CacheComponentsDictionary.TryGetValue(unitId, out entity))
            {
                entity = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<Entity>(unitId, self.key);
                if (entity != null)
                {
                    self.AddOrUpdate(entity);
                }
            }

            return entity;
        }

        public static void Delete(this UnitCache self, long unitId)
        {
            if (self.CacheComponentsDictionary.TryGetValue(unitId, out var entity))
            {
                entity.Dispose();
                self.CacheComponentsDictionary.Remove(unitId);
            }
        }
    }
}