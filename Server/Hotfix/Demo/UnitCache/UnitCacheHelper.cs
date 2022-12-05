namespace ET
{
    public static class UnitCacheHelper
    {
        /// <summary>
        /// 保存或更新Unit缓存
        /// </summary>
        public static async ETTask AddOrUpdateUnitCache<T>(this T self) where T : Entity, IUnitCache
        {
            var message = new Other2UnitCache_AddOrUpdateUnit() { UnitId = self.Id };
            message.EntityTypes.Add(typeof(T).FullName);
            message.EntityBytes.Add(MongoHelper.ToBson(self));
            await MessageHelper.CallActor(StartSceneConfigCategory.Instance.GetUnitCacheConfig(self.Id).InstanceId, message);
        }

        /// <summary>
        /// 获取玩家缓存
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="unitId">玩家unitId</param>
        /// <returns></returns>
        public static async ETTask<Unit> GetUnitCache(Scene scene, long unitId)
        {
            var instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId;
            Other2UnitCache_GetUnit message = new Other2UnitCache_GetUnit() { UnitId = unitId };
            UnitCache2Other_GetUnit queryUnit =(UnitCache2Other_GetUnit) await MessageHelper.CallActor(instanceId, message);
            if (queryUnit.Error != ErrorCode.ERR_Success || queryUnit.EntityList.Count <= 0)
            {
                return null;
            }

            int indexOf = queryUnit.ComponentNameList.IndexOf(nameof (Unit));
            Unit unit = queryUnit.EntityList[indexOf] as Unit;
            if (unit == null)
            {
                return null;
            }

            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            unitComponent.Add(unit);
            foreach (var entity in queryUnit.EntityList)
            {
                if (entity == null || entity is Unit)
                {
                    continue;
                }

                unit.AddComponent(entity);
            }

            return unit;
        }

        /// <summary>
        /// 获取玩家组件缓存
        /// </summary>
        public static async ETTask<T> GetComponentCache<T>(long unitId) where T : Entity, IUnitCache
        {
            var instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId;
            Other2UnitCache_GetUnit message = new Other2UnitCache_GetUnit() { UnitId = unitId };
            message.ComponentNameList.Add(typeof(T).FullName);
            UnitCache2Other_GetUnit queryUnit =(UnitCache2Other_GetUnit) await MessageHelper.CallActor(instanceId, message);
            if (queryUnit.Error == ErrorCode.ERR_Success || queryUnit.EntityList.Count > 0)
            {
                return queryUnit.EntityList[0] as T;
            }

            return null;
        }

        /// <summary>
        /// 删除玩家缓存
        /// </summary>
        /// <param name="unitId"></param>
        public static async ETTask DeleteUnit(long unitId)
        {
            var instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId;
            Other2UnitCache_DeleteUnit message = new Other2UnitCache_DeleteUnit() { UnitId = unitId };
            await MessageHelper.CallActor(instanceId, message);
        }

        /// <summary>
        /// 保存Unit及Unit身上组件到缓存服及数据库中
        /// </summary>
        public static async ETTask AddOrUpdateAllUnitCache(Unit unit)
        {
            var instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unit.Id).InstanceId;
            Other2UnitCache_AddOrUpdateUnit message = new Other2UnitCache_AddOrUpdateUnit() { UnitId = unit.Id };
            message.EntityTypes.Add(typeof(Unit).FullName);
            message.EntityBytes.Add(MongoHelper.ToBson(unit));

            foreach (var component in unit.Components)
            {
                if (!typeof (IUnitCache).IsAssignableFrom(component.Key))
                {
                    continue;
                }
                message.EntityTypes.Add(component.Key.FullName);
                message.EntityBytes.Add(MongoHelper.ToBson(component.Value));
            }

            await MessageHelper.CallActor(instanceId, message);
        }
    }
}