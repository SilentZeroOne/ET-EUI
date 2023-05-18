using System;
using ET.EventType;

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
                var isSelf = unit.Id == self.ZoneScene().GetComponent<PlayerComponent>().MyId;
                var index = isSelf? 0 : unit.GetSeatIndex();
                self.Seats.Add(unit.Id, index);
                self.Units[index] = unit;
                if (isSelf)
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

        public static void SetReady(this LandRoomComponent self, long unitId, int ready)
        {
            var index = self.GetUnitSeatIndex(unitId);
            self.isReady[index] = ready == 1;
            Game.EventSystem.Publish(new UnitReady() { ZoneScene = self.ZoneScene(), UnitIndex = index, Ready = ready });
        }

        public static void SetPromt(this LandRoomComponent self, long unitId, int rob)
        {
            var index = self.GetUnitSeatIndex(unitId);
            
            Game.EventSystem.Publish(new RobLandLord() { ZoneScene = self.ZoneScene(), Rob = rob == 1, UnitIndex = index });
        }

        public static void SetMultiples(this LandRoomComponent self, int multiples)
        {
            self.Multiples = multiples;
            Game.EventSystem.Publish(new SetMutiples() { ZoneScene = self.ZoneScene(), Mutiples = multiples});
        }
        
        public static void LeaveRoom(this LandRoomComponent self, long unitId)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            session.Send(new C2Lo_ReturnLobby() { UnitId = unitId });
        }
    }
}