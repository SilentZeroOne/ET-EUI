using System;

namespace ET
{
    public class Other2UnitCache_AddOrUpdateUnitHandler : AMActorRpcHandler<Scene,Other2UnitCache_AddOrUpdateUnit,UnitCache2Other_AddOrUpdateUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_AddOrUpdateUnit request, UnitCache2Other_AddOrUpdateUnit response, Action reply)
        {
            UpdateUnitCacheAsync(scene, request).Coroutine();
            reply();
            await ETTask.CompletedTask;
        }

        private async ETTask UpdateUnitCacheAsync(Scene scene, Other2UnitCache_AddOrUpdateUnit request)
        {
            var unitCache = scene.GetComponent<UnitCacheComponent>();
            using (ListComponent<Entity> entityList = ListComponent<Entity>.Create())
            {
                for (int i = 0; i < request.EntityTypes.Count; i++)
                {
                    Type type = Game.EventSystem.GetType(request.EntityTypes[i]);
                    Entity entity = (Entity)MongoHelper.FromBson(type, request.EntityBytes[i]);
                    entityList.Add(entity);
                }

                await unitCache.AddOrUpdate(request.UnitId, entityList);
            }
        }
    }
}