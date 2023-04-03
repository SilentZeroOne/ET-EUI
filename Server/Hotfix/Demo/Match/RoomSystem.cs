namespace ET
{
    
    [Timer(TimerType.RoomEmptyCloseTimer)]
    public class RoomEmptyCloseTimerTimer: ATimer<Room>
    {
        public override void Run(Room self)
        {
            if (self.PlayerCount == 0)
            {
                Log.Info($"Room {self.Id} 没有玩家 已释放");
                self.Dispose();
            }
        }
    }
    
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
            self.PlayingScene.Dispose();
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

    [FriendClass(typeof(Room))]
    [FriendClassAttribute(typeof(ET.LandMatchComponent))]
    public static class RoomSystem
    {
        public static async ETTask CreatePlayingScene(this Room self)
        {
            self.PlayingScene = await SceneFactory.Create(self, "PlayingRoom", SceneType.Map);
            self.CloseTimer = TimerComponent.Instance.NewRepeatedTimer(30 * 1000, TimerType.RoomEmptyCloseTimer, self);
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
                foreach (var u in self.Units)
                {
                    if (u is { IsDisposed: false })
                        create_units.Units.Add(UnitHelper.CreateUnitInfo(u));
                }

                unit.GetComponent<NumericComponent>().Set(NumericType.IsWaiting, 1);

                self.Broadcast(create_units);
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
            //更改房间状态
            LandMatchComponent landMatchComponent = self.Parent as LandMatchComponent;
            landMatchComponent.FreeLandlordsDict.Remove(self.Id);
            landMatchComponent.GamingLandlordsDict.Add(self.Id, self);
            
            //更改玩家状态
            foreach (var unit in self.Units)
            {
                landMatchComponent.WaitingUnit.Remove(unit.Id);
                landMatchComponent.PlayingUnit.Add(unit.Id, self);
            }
            
            //TODO 添加斗地主开始必要组件
            
            //开始游戏
        }

        public static int GetUnitSeatIndex(this Room self, long id)
        {
            if (self.Seats.TryGetValue(id, out var index))
            {
                return index;
            }

            return -1;
        }

        public static Unit RemoveUnit(this Room self, long id)
        {
            var index = self.GetUnitSeatIndex(id);
            Unit unit = null;
            if (index >= 0)
            {
                self.Seats.Remove(id);
                self.isReady[index] = false;
                self.Units[index].GetComponent<NumericComponent>().Set(NumericType.IsWaiting, 0);
                self.Units[index].GetComponent<NumericComponent>().Set(NumericType.IsPlaying, 0);
                unit = self.Units[index];
                self.Units[index] = null;
                Log.Info($"Unit {id} 离开room instanceid:{self.InstanceId}");
                
                var unitLeave = new M2C_RemoveUnits();
                unitLeave.Units.Add(id);

                self.Broadcast(unitLeave, unitId: id);
            }
            
            return unit;
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

        public static void Broadcast(this Room self, IActorMessage message, long unitId = 0)
        {
            foreach (var unit in self.Units)
            {
                if (unit is { IsDisposed: false })
                {
                    if (unit.Id != unitId)
                        MessageHelper.SendToClient(unit, message);
                }
            }
        }

        public static bool SetReady(this Room self, long unitId, bool ready = true)
        {
            if (self.Seats.ContainsKey(unitId))
            {
                self.isReady[self.Seats[unitId]] = ready;
                var message = new Lo2C_NotifyUnitReady() { UnitId = unitId, Ready = ready ? 1 : 0 };
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