using BM;
using UnityEngine;

namespace ET
{
    [FriendClass(typeof(GlobalComponent))]
    [FriendClassAttribute(typeof(ET.RigidBody2DComponent))]
    [FriendClassAttribute(typeof(ET.BoundComponent))]
    public class AfterUnitCreate_CreateUnitView : AEvent<EventType.AfterUnitCreate>
    {
        protected override void Run(EventType.AfterUnitCreate args)
        {
            // Unit View层
            // 这里可以改成异步加载，demo就不搞了
            // GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            // GameObject prefab = bundleGameObject.Get<GameObject>("Skeleton");

            GameObject prefab = AssetComponent.Load<GameObject>(args.Unit.Config.PrefabName.StringToAB());

            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            var startPos = args.Unit.Config.StartPosition;
            Vector3 pos = Vector3.zero;
            if (startPos.Length > 0)
                pos = new Vector3(float.Parse(startPos[0]), float.Parse(startPos[1]), float.Parse(startPos[2]));

            args.Unit.AddComponent<GameObjectComponent>().GameObject = go;
            args.Unit.Position = pos;
            
            args.Unit.AddComponent<RigidBody2DComponent>().Rigidbody2D = go.GetComponent<Rigidbody2D>();
            args.Unit.AddComponent<AnimatorComponent>();
            args.Unit.AddComponent<TriggerComponent>();

            if (args.Unit.Config.Type == (int)UnitType.Player)
            {
                args.Unit.AddComponent<OperaComponent>();
                args.Unit.AddComponent<ItemPickerComponent>();
                args.Unit.AddComponent<TeleportComponent>();
            
                var cinemachineComponent = args.Unit.AddComponent<CinemachineComponent, GameObject>(go.Get<GameObject>("CM vcam1"));

                cinemachineComponent.SetConfinerBounding(args.Unit.ZoneScene().CurrentScene().GetComponent<BoundComponent>().Bounds);
            }
        }
    }
}