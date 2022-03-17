using System;

namespace ET
{
    public static class UnitCacheHelper
    {
        /// <summary>
        /// 保存或更新玩家缓存
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static async ETTask AddOrUpdateUnitCache<T>(this T self) where T : Entity, IUnitCache
        {
            var message = new Other2UnitCache_AddOrUpdateUnit() { UnitId = self.Id };
            message.EntityTypes.Add(typeof (T).FullName);
            message.EntityBytes.Add(MongoHelper.ToBson(self));
            await MessageHelper.CallActor(StartSceneConfigCategory.Instance.GetUnitCacheConfig(self.Id).InstanceId, message);
        }

        public static async ETTask<Unit> GetUnitCache(Scene scene, long unitId)
        {
            long instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId;
            Other2UnitCache_GetUnit message = new Other2UnitCache_GetUnit() { UnitId = unitId };
            UnitCache2Other_GetUnit queryResponse = (UnitCache2Other_GetUnit)await MessageHelper.CallActor(instanceId, message);
            if (queryResponse.Error != ErrorCode.ERR_Success || queryResponse.EntityList.Count <= 0)
            {
                return null;
            }

            int indexOf = queryResponse.ComponentNameList.IndexOf(nameof (Unit));
            Unit unit = queryResponse.EntityList[indexOf] as Unit;
            if (unit == null)
            {
                return null;
            }
            
            queryResponse.EntityList.Remove(unit);
            scene.AddChild(unit);
            foreach (var entity in queryResponse.EntityList)
            {
                if (entity == null || entity.IsDisposed || entity == unit)
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
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async ETTask<T> GetUnitComponentCache<T>(this T self) where T : Entity, IUnitCache
        {
            var message = new Other2UnitCache_GetUnit() { UnitId = self.Id };
            message.ComponentNameList.Add(typeof (T).Name);
            var instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(self.Id).InstanceId;
            UnitCache2Other_GetUnit response = (UnitCache2Other_GetUnit)await MessageHelper.CallActor(instanceId, message);
            if (response.Error == ErrorCode.ERR_Success && response.EntityList.Count > 0)
            {
                return response.EntityList[0] as T;
            }

            return null;
        }

        public static async ETTask DeleteUnitCache(long unitId)
        {
            var message = new Other2UnitCache_DeleteUnit() { UnitId = unitId };
            await MessageHelper.CallActor(StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId, message);
        }

        public static void AddOrUpdateAllUnitCache(Unit unit)
        {
            var message = new Other2UnitCache_AddOrUpdateUnit() { UnitId = unit.Id };
            message.EntityTypes.Add(unit.GetType().FullName);
            message.EntityBytes.Add(MongoHelper.ToBson(unit));

            foreach ((Type key,Entity value) in unit.Components)
            {
                if (!typeof (IUnitCache).IsAssignableFrom(key))
                {
                    continue;
                }
                message.EntityTypes.Add(key.FullName);
                message.EntityBytes.Add(MongoHelper.ToBson(value));
            }
            MessageHelper.CallActor(StartSceneConfigCategory.Instance.GetUnitCacheConfig(unit.Id).InstanceId,message).Coroutine();
        }
    }
}