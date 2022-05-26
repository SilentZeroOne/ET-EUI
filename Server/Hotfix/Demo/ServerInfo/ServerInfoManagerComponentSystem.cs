namespace ET
{
    public class ServerInfoManagerComponentAwakeSystem: AwakeSystem<ServerInfoManagerComponent>
    {
        public override void Awake(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }
    
    public class ServerInfoManagerComponentDestroySystem : DestroySystem<ServerInfoManagerComponent>
    {
        public override void Destroy(ServerInfoManagerComponent self)
        {
            foreach (var serverInfo in self.ServerInfos)
            {
                serverInfo?.Dispose();
            }
            self.ServerInfos.Clear();
        }
    }
    
    public class ServerInfoManagerComponentLoadSystem : LoadSystem<ServerInfoManagerComponent>
    {
        public override void Load(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }

    [FriendClass(typeof(ServerInfo))]
    [FriendClass(typeof(ServerInfoManagerComponent))]
    public static class ServerInfoManagerComponentSystem
    {
        public static async ETTask Awake(this ServerInfoManagerComponent self)
        {
            var serverInfoList = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<ServerInfo>(d => true);
            if (serverInfoList == null || serverInfoList.Count <= 0)
            {
                Log.Error("ServerInfo count is zero");
                var serverInfos = ServerInfosConfigCategory.Instance.GetAll();
                foreach (var serverInfo in serverInfos.Values)
                {
                    var info = self.AddChildWithId<ServerInfo>(serverInfo.Id);
                    info.Status = (int) ServerStatus.Normal;
                    info.ServerName = serverInfo.ServerName;
                    self.ServerInfos.Add(info);

                    await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(info);
                }

                return;
            }
            
            self.ServerInfos.Clear();

            foreach (var serverInfo in serverInfoList)
            {
                self.AddChild(serverInfo);
                self.ServerInfos.Add(serverInfo);
            }

            await ETTask.CompletedTask;
        }
        
    }
}