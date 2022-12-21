using System.Linq;

namespace ET
{
    public class LandMatchComponentAwakeSystem: AwakeSystem<LandMatchComponent>
    {
        public override void Awake(LandMatchComponent self)
        {

        }
    }

    public class LandMatchComponentDestroySystem: DestroySystem<LandMatchComponent>
    {
        public override void Destroy(LandMatchComponent self)
        {

        }
    }

    [FriendClass(typeof (LandMatchComponent))]
    public static class LandMatchComponentSystem
    {
        public static Room GetGamingRoom(this LandMatchComponent self, long id)
        {
            self.PlayingUnit.TryGetValue(id, out var room);
            return room;
        }

        public static Room GetWaitingRoom(this LandMatchComponent self, long id)
        {
            self.WaitingUnit.TryGetValue(id, out var room);
            return room;
        }

        public static Room GetFreeLandlordRoom(this LandMatchComponent self)
        {
            return self.FreeLandlordsDict.Values.FirstOrDefault(r => r.PlayerCount < 3);
        }

        public static void AddUnitToMatchingQueue(this LandMatchComponent self, Unit unit)
        {
            self.MatchingQueue.Enqueue(unit);
            Log.Info($"Unit {unit.Id} 进入匹配队列");
            //广播通知所有匹配中的玩家
            self.Broadcast(new M2C_UpdateLandMatcher() { CurrentQueueCount = self.MatchingQueue.Count });
            
            self.MatchingCheck();
        }

        public static void MatchingCheck(this LandMatchComponent self)
        {
            Room room = self.GetFreeLandlordRoom();
            if (room != null)
            {
                while (self.MatchingQueue.Count > 0 && room.PlayerCount < 3)
                {
                    self.JoinRoom(room, self.MatchingQueue.Dequeue());
                }
            }
            else
            {
                room = self.AddChild<Room>();
                self.FreeLandlordsDict.Add(room.Id, room);
                while (self.MatchingQueue.Count > 0 && room.PlayerCount < 3)
                {
                    self.JoinRoom(room, self.MatchingQueue.Dequeue());
                }
            }
        }

        public static void JoinRoom(this LandMatchComponent self, Room room, Unit unit)
        {
            if (unit is { IsDisposed: true })
            {
                return;
            }
            
            self.WaitingUnit[unit.Id] = room;
            room.AddUnit(unit).Coroutine();
            //广播通知所有匹配中的玩家
            self.Broadcast(new M2C_UpdateLandMatcher() { CurrentQueueCount = self.MatchingQueue.Count });
            
            Log.Info($"玩家 {unit.Id} 进入房间 {room.Id}");
        }

        /// <summary>
        /// 往匹配队列广播
        /// </summary>
        /// <param name="self"></param>
        /// <param name="actorMessage"></param>
        public static void Broadcast(this LandMatchComponent self, IActorMessage actorMessage)
        {
            foreach (var unit in self.MatchingQueue)
            {
                MessageHelper.SendToClient(unit, actorMessage);
            }
        }

        public static void LeaveMatchingQueue(this LandMatchComponent self,long unitId)
        {
            Log.Info($"{unitId} 离开匹配队列");
            ListComponent<Unit> temp = ListComponent<Unit>.Create();
            temp.AddRange(self.MatchingQueue);
            foreach (var unit in temp)
            {
                if (unit.Id == unitId)
                {
                    temp.Remove(unit);
                    break;
                }
            }
            
            self.MatchingQueue.Clear();
            foreach (var unit in temp)
            {
                self.MatchingQueue.Enqueue(unit);
            }
            temp.Dispose();
            
            //广播通知所有匹配中的玩家
            self.Broadcast(new M2C_UpdateLandMatcher() { CurrentQueueCount = self.MatchingQueue.Count });
        }

        public static void RemoveUnit(this LandMatchComponent self, long id)
        {
            if (self.WaitingUnit.ContainsKey(id))
            {
                self.WaitingUnit[id].RemoveUnit(id);
                self.WaitingUnit.Remove(id);
            }

            if (self.PlayingUnit.ContainsKey(id))
            {
                self.PlayingUnit[id].RemoveUnit(id);
                self.PlayingUnit.Remove(id);
            }
            
            self.LeaveMatchingQueue(id);
        }
    }
}