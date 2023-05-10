

using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClassAttribute(typeof(ET.OrderControllerComponent))]
    public class C2Lo_RobLandLordRequestHandler : AMActorLocationRpcHandler<Unit, C2Lo_RobLandLordRequest, Lo2C_RobLandLordResponse>
    {
        protected override async ETTask Run(Unit unit, C2Lo_RobLandLordRequest request, Lo2C_RobLandLordResponse response, Action reply)
        {
            Scene zoneScene = unit.DomainScene().Parent.DomainScene();
            LandMatchComponent landMatchComponent = zoneScene.GetComponent<LandMatchComponent>();
            Room room = landMatchComponent.GetGamingRoom(request.UnitId);
            GameControllerComponent gameControllerComponent = room.GetComponent<GameControllerComponent>();
            OrderControllerComponent orderControllerComponent = room.GetComponent<OrderControllerComponent>();

            if (orderControllerComponent.CurrentPlayer == request.UnitId)
            {
                var rob = request.Rob == 1;
                orderControllerComponent.GameLordState[request.UnitId] = rob;
                if (rob)
                {
                    orderControllerComponent.Biggest = request.UnitId;
                    gameControllerComponent.Multiples *= 2; //倍率翻倍
                    room.Broadcast(new Lo2C_SetMultiples() { Multiples = gameControllerComponent.Multiples * room.Config.Multiples });
                }
                
                reply();
                
                room.Broadcast(new Lo2C_RobLandLord()
                {
                    UnitId = request.UnitId,
                    Rob = request.Rob
                });

                //房间有三个人抢地主操作后（抢或不抢）
                Log.Debug($"Select lordIndex {orderControllerComponent.SelectLordIndex}");
                if (orderControllerComponent.SelectLordIndex >= room.PlayerCount)
                {
                    if (orderControllerComponent.Biggest == 0)
                    {
                        //没人抢地主 重新发牌
                        gameControllerComponent.BackToDeck();
                        
                        gameControllerComponent.StartGame();
                        
                        return;
                    }
                    Log.Debug($"Select {orderControllerComponent.SelectLordIndex}  roomPlayer{room.PlayerCount} Biggest {orderControllerComponent.Biggest}");
                    if ((orderControllerComponent.SelectLordIndex == room.PlayerCount &&
                            ((orderControllerComponent.Biggest != orderControllerComponent.FirstAuthority.Key &&
                                    !orderControllerComponent.FirstAuthority.Value) ||
                                orderControllerComponent.Biggest == orderControllerComponent.FirstAuthority.Key)) ||
                        orderControllerComponent.SelectLordIndex > room.PlayerCount)
                    {
                        //选完地主 正式开始
                        
                        gameControllerComponent.CardsOnTable(orderControllerComponent.Biggest);
                        return;
                    }
                }

                if (request.UnitId == orderControllerComponent.FirstAuthority.Key && rob)
                {
                    orderControllerComponent.FirstAuthority = new KeyValuePair<long, bool>(request.UnitId, true);
                }

                orderControllerComponent.Turn();
                orderControllerComponent.SelectLordIndex++;
                //当前已选完 换下一个人选择
                room.Broadcast(new Lo2C_CurrentPlayer()
                {
                    ActionType = (int)ActionType.RobLandLord, UnitId = orderControllerComponent.CurrentPlayer
                });
            }
            
            await ETTask.CompletedTask;
        }
    }
}