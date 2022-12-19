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
        
        public static void AddUnit(this Room self, Unit unit)
        {
            if (self.PlayerCount < 3 && !self.Seats.ContainsKey(unit.Id))
            {
                var index = self.Seats.Count;
                self.Seats.Add(unit.Id, index);
                self.Units[index] = unit;

                TransferHelper.Transfer(unit, self.PlayingScene.InstanceId, "PlayingRoom").Coroutine();
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

        public static void Broadcast(this Room self, IActorMessage message)
        {
            foreach (var unit in self.Units)
            {
                if (unit != null)
                    MessageHelper.SendToClient(unit, message);
            }
        }
    }
}