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

            var currentScene = args.Unit.ZoneScene().CurrentScene();
            GameObject unitObject = AssetComponent.Load<GameObject>(BPath.Assets_Bundles_ResBundles_Unit_LandLordUnit__prefab);
            GameObject unitGo = UnityEngine.Object.Instantiate(unitObject, GlobalComponent.Instance.Unit, true);
            
            LandRoomObjectsComponent landRoomObjectsComponent = currentScene.GetComponent<LandRoomObjectsComponent>();
            if (landRoomObjectsComponent == null)
            {
                Log.Debug($"Waiting Landroom objects");
                await currentScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_PlayRoomAddObjectComponentFinish>();
                landRoomObjectsComponent = currentScene.GetComponent<LandRoomObjectsComponent>();
                Log.Debug($"Landroom objects loaded");
            }

            args.Unit.AddComponent<GameObjectComponent>().SetGameObject(unitGo);
            args.Unit.AddComponent<SpriteRendererComponent>().Renderer = unitGo.GetComponent<SpriteRenderer>();

            var poker = AssetComponent.Load<SpriteAtlas>(BPath.Assets_Bundles_ResBundles_Atlas_Pokers__spriteatlas);
            var role2 = poker.GetSprite("Role2");
            unitGo.GetComponent<SpriteRenderer>().sprite = role2;
            
            PlayerComponent playerComponent = args.Unit.ZoneScene().GetComponent<PlayerComponent>();
            if (playerComponent is { IsDisposed: false })
            {
                if (playerComponent.MyId == args.Unit.Id)
                {
                    unitGo.transform.position = landRoomObjectsComponent.SelfUnitPosition.position;
                    unitGo.transform.position = landRoomObjectsComponent.SelfUnitPosition.position;
                }
                else
                {
                    var usedList = landRoomObjectsComponent.PositionUsed;
                    for (int i = 0; i < usedList.Length; i++)
                    {
                        if (!usedList[i])
                        {
                            unitGo.transform.position = landRoomObjectsComponent.OtherUnitPositions[i].position;
                            landRoomObjectsComponent.PositionUsed[i] = true;
                            landRoomObjectsComponent.Seats.Add(args.Unit.Id, i);
                            if (i == 0)
                            {
                                args.Unit.GetComponent<SpriteRendererComponent>().Renderer.flipX = true;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}