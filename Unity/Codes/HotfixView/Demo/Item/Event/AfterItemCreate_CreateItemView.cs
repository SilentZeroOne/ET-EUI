using BM;
using ET.EventType;
using UnityEngine;

namespace ET
{
    [FriendClassAttribute(typeof(ET.Item))]
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
            if (a.Item.GetComponent<GameObjectComponent>() == null)
            {
                a.Item.AddComponent<GameObjectComponent>();
            }

            a.Item.GetComponent<GameObjectComponent>().GameObject = go;

            if (a.Item.GetComponent<ItemViewComponent>() == null)
            {
                a.Item.AddComponent<ItemViewComponent>();
            }
            else
            {
                a.Item.GetComponent<ItemViewComponent>().Init().Coroutine();
            }

            go.transform.position = a.UsePos ? new Vector3(a.X, a.Y)
                    : new Vector3(RandomHelper.RandomNumber(-5, 5), RandomHelper.RandomNumber(-5, 5), 0);
            a.Item.Position = go.transform.position;
            
            if (a.SaveInScene)
                a.Item.ZoneScene().CurrentScene().GetComponent<ItemsComponent>().SaveItemsComponent();
        }
    }
}