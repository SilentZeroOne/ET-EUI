﻿using System;

namespace ET
{
    [FriendClass(typeof(SessionStateComponent))]
    [FriendClass(typeof(SessionPlayerComponent))]
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    [FriendClassAttribute(typeof(ET.UnitGateComponent))]
    public class C2G_EnterGameHandler : AMRpcHandler<C2G_EnterGame, G2C_EnterGame>
    {
        protected override async ETTask Run(Session session, C2G_EnterGame request, G2C_EnterGame response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Gate)
            {
                Log.Error($"请求的Scene错误，当前Scene为{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RepeatedRequestError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            var sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
            if (sessionPlayerComponent == null)
            {
                response.Error = ErrorCode.ERR_SessionPlayerError;
                reply();
                return;
            }

            Player player = Game.EventSystem.Get(sessionPlayerComponent.PlayerInstanceId) as Player;
            if (player == null || player.IsDisposed)
            {
                response.Error = ErrorCode.ERR_NonePlayerError;
                reply();
                return;
            }

            long sessionInstanceId = session.InstanceId;
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, player.Account.GetHashCode()))
                {
                    if (sessionInstanceId != session.InstanceId || player.IsDisposed)
                    {
                        response.Error = ErrorCode.ERR_SessionPlayerError;
                        reply();
                        return;
                    }

                    if (session.GetComponent<SessionStateComponent>() != null &&
                        session.GetComponent<SessionStateComponent>().State == SessionState.Game)
                    {
                        response.Error = ErrorCode.ERR_SessionStateError;
                        reply();
                        return;
                    }

                    if (player.PlayerState == PlayerState.Game)
                    {
                        try
                        {
                            IActorResponse reqEnter = await MessageHelper.CallLocationActor(player.UnitId, new G2M_RequestEnterGameState());
                            if (reqEnter.Error == ErrorCode.ERR_Success)
                            {
                                reply();
                                return;
                            }

                            Log.Error($"二次登陆失败 {reqEnter.Error.ToString()}|{reqEnter.Message}");
                            response.Error = ErrorCode.ERR_ReEnterGameError;
                            await DisconnectHelper.KickPlayer(player, true);
                            reply();
                            session.Disconnect().Coroutine();
                        }
                        catch (Exception e)
                        {
                            Log.Error($"二次登陆失败 {e.ToString()}");
                            response.Error = ErrorCode.ERR_ReEnterGameError2;
                            await DisconnectHelper.KickPlayer(player, true);
                            reply();
                            session.Disconnect().Coroutine();
                            throw;
                        }
                    }

                    try
                    {
                        // var gateMapComponent = player.AddComponent<GateMapComponent>();
                        // gateMapComponent.Scene = await SceneFactory.Create(gateMapComponent, "GateMap", SceneType.Map);

                        //Unit unit = UnitFactory.Create(gateMapComponent.Scene, player.Id, UnitType.Player);
                        (bool isNewPlayer, Unit unit) = await UnitHelper.LoadUnit(player);

                        unit.AddComponent<UnitGateComponent, long>(player.InstanceId);//现在还在Gate上，需要转移到Map上
                        
                        //初始化
                        await UnitHelper.InitUnit(unit, isNewPlayer);

                        player.ChatInfoUnitInstanceId = await this.EnterWorldChatServer(unit);
                        
                        response.MyId = unit.Id;
                        reply();

                        StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Game");
                        await TransferHelper.Transfer(unit, startSceneConfig.InstanceId, startSceneConfig.Name);

                        SessionStateComponent sessionStateComponent = session.GetComponent<SessionStateComponent>();
                        sessionStateComponent.State = SessionState.Game;
                        player.PlayerState = PlayerState.Game;

                    }
                    catch (Exception e)
                    {
                        Log.Error($"角色进入游戏逻辑服发生错误 账号ID:{player.Account} 角色ID:{player.Id} 异常信息 {e.ToString()}");
                        response.Error = ErrorCode.ERR_EnterGameError;
                        reply();
                        await DisconnectHelper.KickPlayer(player, true);
                        session.Disconnect().Coroutine();
                    }
                }
            }

            await ETTask.CompletedTask;
        }

        private async ETTask<long> EnterWorldChatServer(Unit unit)
        {
            StartSceneConfig config = StartSceneConfigCategory.Instance.GetBySceneName(unit.DomainZone(), "ChatInfo");
            Chat2G_EnterChat message = (Chat2G_EnterChat)await MessageHelper.CallActor(config.InstanceId,
                new G2Chat_EnterChat()
                {
                    Name = unit.GetComponent<RoleInfo>().Name,
                    UnitId = unit.Id,
                    GateSessionActorId = unit.GetComponent<UnitGateComponent>().GateSessionActorId
                });

            return message.ChatInfoUnitInstanceId;
        }
    }
}