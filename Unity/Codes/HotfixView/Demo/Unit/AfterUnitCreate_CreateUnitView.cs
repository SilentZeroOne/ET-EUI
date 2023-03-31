﻿using BM;
using UnityEngine;
using UnityEngine.U2D;

namespace ET
{
    [FriendClass(typeof(GlobalComponent))]
    [FriendClassAttribute(typeof(ET.LandRoomObjectsComponent))]
    [FriendClassAttribute(typeof(ET.SpriteRendererComponent))]
    public class AfterUnitCreate_CreateUnitView : AEvent<EventType.AfterUnitCreate>
    {
        protected override async void Run(EventType.AfterUnitCreate args)
        {
            // Unit View层
            // 这里可以改成异步加载，demo就不搞了
            if (!args.CreateView) return;

            GameObject unitObject = AssetComponent.Load<GameObject>(BPath.Assets_Bundles_ResBundles_Unit_LandLordUnit__prefab);
            GameObject unitGo = UnityEngine.Object.Instantiate(unitObject, GlobalComponent.Instance.Unit, true);
            
            await args.Unit.ZoneScene().CurrentScene().GetComponent<ObjectWait>().Wait<WaitType.Wait_PlayRoomAddObjectComponentFinish>();

            LandRoomObjectsComponent landRoomObjectsComponent = args.Unit.ZoneScene().CurrentScene().GetComponent<LandRoomObjectsComponent>();

            args.Unit.AddComponent<GameObjectComponent>().GameObject = unitGo;
            args.Unit.AddComponent<SpriteRendererComponent>().Renderer = unitGo.GetComponent<SpriteRenderer>();

            var poker = AssetComponent.Load<SpriteAtlas>(BPath.Assets_Bundles_ResBundles_Atlas_Pokers__spriteatlas);
            var role2 = poker.GetSprite("Role2");
            unitGo.GetComponent<SpriteRenderer>().sprite = role2;
            
            RoleInfoComponent roleInfoComponent = args.Unit.ZoneScene().GetComponent<RoleInfoComponent>();
            if (roleInfoComponent.RoleInfo is { IsDisposed: false })
            {
                if (roleInfoComponent.RoleInfo.Id == args.Unit.Id)
                {
                    unitGo.transform.position = landRoomObjectsComponent.SelfUnitPosition.position;
                }
                else
                {
                    var usedList = landRoomObjectsComponent.PositionUsed;
                    for (int i = 0; i < usedList.Length; i++)
                    {
                        if (usedList[i])
                        {
                            unitGo.transform.position = landRoomObjectsComponent.OtherUnitPositions[i].position;
                            landRoomObjectsComponent.PositionUsed[i] = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}