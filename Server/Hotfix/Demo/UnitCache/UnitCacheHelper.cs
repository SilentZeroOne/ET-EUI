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
    }
}