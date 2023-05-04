using System;

namespace ET
{
    public class LandRoomComponentAwakeSystem: AwakeSystem<LandRoomComponent>
    {
        public override void Awake(LandRoomComponent self)
        {
            self.AddComponent<HandCardsComponent>();
        }
    }

    public class LandRoomComponentDestroySystem: DestroySystem<LandRoomComponent>
    {
        public override void Destroy(LandRoomComponent self)
        {
            for (int i = 0; i < 3; i++)
            {
                self.Units[i]?.Dispose();
                self.Units[i] = null;
                self.isReady[i] = false;
            }
        }
    }

    [FriendClass(typeof (LandRoomComponent))]
    public static class LandRoomComponentSystem
    {
        public static void AddUnit(this LandRoomComponent self, Unit unit)
        {
            if (self.PlayerCount < 3 && !self.Seats.ContainsKey(unit.Id))
            {
                var index = self.Seats.Count;
                self.Seats.Add(unit.Id, index);
                self.Units[index] = unit;
                if (unit.Id == self.ZoneScene().GetComponent<PlayerComponent>().MyId)
                    unit.AddComponent<HandCardsComponent>();
            }
            else
            {
                Log.Error($"无法加入房间 {self.Id}");
            }
        }

        public static void RemoveUnit(this LandRoomComponent self, long id)
        {
            if (self.Seats.ContainsKey(id))
            {
                var index = self.GetUnitSeatIndex(id);
                self.Seats.Remove(id);
                self.Units[index] = null;
                self.isReady[index] = false;
                Game.EventSystem.Publish(new EventType.UnitReady() { ZoneScene = self.ZoneScene(), UnitIndex = index, Ready = 0 });
            }
            else
            {
                Log.Error($"用户 {id} 不在房间{self.Id}中 无法移除");
            }
        }
        
        public static int GetUnitSeatIndex(this LandRoomComponent self, long id)
        {
            if (self.Seats.TryGetValue(id, out var index))
            {
                return index;
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="unitId"></param>
        /// <param name="ready">0 = unready 1 = ready</param>
        /// <returns></returns>
        public static async ETTask<int> SetReadyNetwork(this LandRoomComponent self, long unitId, int ready)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            Lo2C_UnitReady lo2CUnitReady;
            try
            {
                lo2CUnitReady = (Lo2C_UnitReady)await session.Call(new C2Lo_UnitReady() { UnitId = unitId , Ready = ready});
                if (lo2CUnitReady.Error != ErrorCode.ERR_Success)
                {
                    Log.Error(lo2CUnitReady.Error.ToString());
                    return lo2CUnitReady.Error;
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            self.SetReady(unitId, ready);

            return ErrorCode.ERR_Success;
        }

        public static void SetReady(this LandRoomComponent self, long unitId, int ready)
        {
            var index = self.GetUnitSeatIndex(unitId);
            self.isReady[index] = ready == 1;
            Game.EventSystem.Publish(new EventType.UnitReady() { ZoneScene = self.ZoneScene(), UnitIndex = index, Ready = ready });
        }

        public static void LeaveRoom(this LandRoomComponent self, long unitId)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            session.Send(new C2Lo_ReturnLobby() { UnitId = unitId });
        }
    }
}