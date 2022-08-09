using BM;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class AfterItemCreate_CreateItemView : AEvent<AfterItemCreate>
    {
        protected override void Run(AfterItemCreate a)
        {
            RunAsync(a).Coroutine();
        }

        private async ETTask RunAsync(AfterItemCreate a)
        {
            GameObject prefab = await AssetComponent.LoadAsync<GameObject>(BPath.Assets_Bundles_ResBundles_ItemPrefab_ItemBase__prefab);

            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);

            a.Item.AddComponent<GameObjectComponent>().GameObject = go;
            go.AddComponent<MonoBridge>().BelongToEntityId = a.Item.InstanceId;
            a.Item.AddComponent<ItemViewComponent>();
            go.transform.position = new Vector3(2, 3, 0);
        }
    }
}