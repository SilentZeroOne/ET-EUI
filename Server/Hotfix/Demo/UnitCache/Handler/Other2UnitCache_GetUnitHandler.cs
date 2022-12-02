using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClassAttribute(typeof(ET.UnitCacheComponent))]
    public class Other2UnitCache_GetUnitHandler : AMActorRpcHandler<Scene, Other2UnitCache_GetUnit, UnitCache2Other_GetUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_GetUnit request, UnitCache2Other_GetUnit response, Action reply)
        {
            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            if (unitCacheComponent == null)
            {
                Log.Error($"No UnitCacheComponent in {scene.Name}");
                reply();
                return;
            }

            Dictionary<string, Entity> dictionary = MonoPool.Instance.Fetch<Dictionary<string, Entity>>();

            try
            {
                if (request.ComponentNameList.Count == 0)
                {
                    dictionary.Add(nameof(Unit), null);
                    foreach (var key in unitCacheComponent.UnitCacheKeyList)
                    {
                        dictionary.Add(key, null);
                    }
                }
                else
                {
                    foreach (var key in request.ComponentNameList)
                    {
                        dictionary.Add(key, null);
                    }
                }

                foreach (var key in dictionary.Keys)
                {
                    dictionary[key] = await unitCacheComponent.Get(request.UnitId, key);
                }
                
                response.EntityList.AddRange(dictionary.Values);
                response.ComponentNameList.AddRange(dictionary.Keys);
            }
            finally
            {
                dictionary.Clear();
                MonoPool.Instance.Recycle(dictionary);
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}