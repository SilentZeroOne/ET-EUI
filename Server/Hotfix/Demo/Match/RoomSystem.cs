namespace ET
{
    public class RoomAwakeSystem: AwakeSystem<Room>
    {
        public override void Awake(Room self)
        {
            self.CreatePlayingScene().Coroutine();
        }
    }

    public class RoomDestroySystem: DestroySystem<Room>
    {
        public override void Destroy(Room self)
        {
            self.PlayingScene = null;
            self.Seats.Clear();
            for (int i = 0; i < 3; i++)
            {
                self.Units[i]?.Dispose();
                self.Units[i] = null;
                self.isReady[i] = false;
            }
        }
    }

    [FriendClass(typeof (Room))]
    public static class RoomSystem
    {
        public static async ETTask CreatePlayingScene(this Room self)
        {
            self.PlayingScene = await SceneFactory.Create(self, "PlayingRoom", SceneType.Map);
        }
        
        public static async ETTask AddUnit(this Room self, Unit unit)
        {
            if (self.PlayerCount < 3 && !self.Seats.ContainsKey(unit.Id))
            {
                var unitId = unit.Id;
                var index = self.Seats.Count;
                self.Seats.Add(unit.Id, index);
                
                await TransferHelper.Transfer(unit, self.PlayingScene.InstanceId, "PlayingRoom");
                
                unit = self.PlayingScene.GetComponent<UnitComponent>().Get(unitId);
                self.Units[index] = unit;
                
                var create_units = new M2C_CreateUnits();
                create_units.Units.Add(UnitHelper.CreateUnitInfo(unit));

                self.Broadcast(create_units, unitId: unit.Id);
            }
            else
            {
                Log.Error($"无法加入房间 {self.Id}");
            }
        }

        public static bool IsGameCanStart(this Room self)
        {
            bool isOk = false;
            for (int i = 0; i < 3; i++)
            {
                isOk |= self.isReady[i];
            }

            return isOk;
        }

        public static void GameStart(this Room self)
        {
            
        }

        public static int GetUnitSeatIndex(this Room self, long id)
        {
            if (self.Seats.TryGetValue(id, out var index))
            {
                return index;
            }

            return -1;
        }

        public static void RemoveUnit(this Room self, long id)
        {
            var index = self.GetUnitSeatIndex(id);
            if (index >= 0)
            {
                self.Seats.Remove(id);
                self.isReady[index] = false;
                self.Units[index] = null;
            }
        }

        public static int GetEmptySeatIndex(this Room self)
        {
            for (int i = 0; i < 3; i++)
            {
                if (self.Units[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }

        public static void Broadcast(this Room self, IActorMessage message ,bool containSelf = false,long unitId = 0)
        {
            foreach (var unit in self.Units)
            {
                if (unit is { IsDisposed: false })
                {
                    if(containSelf)
                        MessageHelper.SendToClient(unit, message);
                    else
                    {
                        if (unit.Id != unitId)
                            MessageHelper.SendToClient(unit, message);
                    }
                }
            }
        }
        
        public static bool SetReady(this Room self, long unitId, bool ready = true)
        {
            if (self.Seats.ContainsKey(unitId))
            {
                self.isReady[self.Seats[unitId]] = ready;
                var message = new Lo2C_NotifyUnitReady() { UnitId = unitId, Ready = ready? 1 : 0 };
                self.Broadcast(message, unitId: unitId);
            }
            else
            {
                Log.Error($"{unitId} 不在当前房间中");
                return false;
            }

            return true;
        }
    }
}