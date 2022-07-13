﻿using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [FriendClass(typeof(Unit))]
    [FriendClass(typeof(MoveComponent))]
    [FriendClass(typeof(NumericComponent))]
    [FriendClass(typeof(GateMapComponent))]
    public static class UnitHelper
    {
        public static UnitInfo CreateUnitInfo(Unit unit)
        {
            UnitInfo unitInfo = new UnitInfo();
            NumericComponent nc = unit.GetComponent<NumericComponent>();
            unitInfo.UnitId = unit.Id;
            unitInfo.ConfigId = unit.ConfigId;
            unitInfo.Type = (int)unit.Type;
            Vector3 position = unit.Position;
            unitInfo.X = position.x;
            unitInfo.Y = position.y;
            unitInfo.Z = position.z;
            Vector3 forward = unit.Forward;
            unitInfo.ForwardX = forward.x;
            unitInfo.ForwardY = forward.y;
            unitInfo.ForwardZ = forward.z;

            // MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            // if (moveComponent != null)
            // {
            //     if (!moveComponent.IsArrived())
            //     {
            //         unitInfo.MoveInfo = new MoveInfo();
            //         for (int i = moveComponent.N; i < moveComponent.Targets.Count; ++i)
            //         {
            //             Vector3 pos = moveComponent.Targets[i];
            //             unitInfo.MoveInfo.X.Add(pos.x);
            //             unitInfo.MoveInfo.Y.Add(pos.y);
            //             unitInfo.MoveInfo.Z.Add(pos.z);
            //         }
            //     }
            // }

            foreach ((int key, long value) in nc.NumericDic)
            {
                unitInfo.Ks.Add(key);
                unitInfo.Vs.Add(value);
            }

            return unitInfo;
        }
        
        // 获取看见unit的玩家，主要用于广播
        public static Dictionary<long, AOIEntity> GetBeSeePlayers(this Unit self)
        {
            return self.GetComponent<AOIEntity>().GetBeSeePlayers();
        }
        
        public static void NoticeUnitAdd(Unit unit, Unit sendUnit)
        {
            M2C_CreateUnits createUnits = new M2C_CreateUnits();
            createUnits.Units.Add(CreateUnitInfo(sendUnit));
            MessageHelper.SendToClient(unit, createUnits);
        }
        
        public static void NoticeUnitRemove(Unit unit, Unit sendUnit)
        {
            M2C_RemoveUnits removeUnits = new M2C_RemoveUnits();
            removeUnits.Units.Add(sendUnit.Id);
            MessageHelper.SendToClient(unit, removeUnits);
        }

        public static async ETTask<(bool, Unit)> LoadUnit(Player player)
        {
            var gateMapComponent = player.AddComponent<GateMapComponent>();
            gateMapComponent.Scene = await SceneFactory.Create(gateMapComponent, "GateMap", SceneType.Map);

            Unit unit = await UnitCacheHelper.GetUnitCache(gateMapComponent.Scene, player.UnitId);

            bool isNewPlayer = unit == null;
            if (isNewPlayer)
            {
                unit = UnitFactory.Create(gateMapComponent.Scene, player.UnitId, UnitType.Player);
                UnitCacheHelper.AddOrUpdateAllUnitCache(unit);
            }

            return (isNewPlayer, unit);
        }

        public static async ETTask InitUnit(Unit unit, bool isNew)
        {
            if (!isNew)
            {
                var numeric = await unit.AddComponent<NumericComponent>().GetUnitComponentCache();
                unit.RemoveComponent<NumericComponent>();
                unit.AddComponent(numeric);

                var bagComponent = await unit.AddComponent<BagComponent>().GetUnitComponentCache();
                unit.RemoveComponent<BagComponent>();
                unit.AddComponent(bagComponent);

                var equipmentsComponent = await unit.AddComponent<EquipmentsComponent>().GetUnitComponentCache();
                unit.RemoveComponent<EquipmentsComponent>();
                unit.AddComponent(equipmentsComponent);
            }
            await ETTask.CompletedTask;
        }
    }
}