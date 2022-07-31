using BM;
using UnityEngine;

namespace ET
{
    [FriendClass(typeof(GlobalComponent))]
    [FriendClassAttribute(typeof(ET.RigidBody2DComponent))]
    public class AfterUnitCreate_CreateUnitView : AEvent<EventType.AfterUnitCreate>
    {
        protected override void Run(EventType.AfterUnitCreate args)
        {
            // Unit View层
            // 这里可以改成异步加载，demo就不搞了
            // GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            // GameObject prefab = bundleGameObject.Get<GameObject>("Skeleton");

            GameObject prefab = AssetComponent.Load<GameObject>(BPath.Assets_Bundles_ResBundles_Unit_Player__prefab);

            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            go.transform.position = args.Unit.Position;
            args.Unit.AddComponent<GameObjectComponent>().GameObject = go;
            args.Unit.AddComponent<RigidBody2DComponent>().Rigidbody2D = go.GetComponent<Rigidbody2D>();
            args.Unit.AddComponent<OperaComponent>();
            //args.Unit.AddComponent<AnimatorComponent>();
        }
    }
}