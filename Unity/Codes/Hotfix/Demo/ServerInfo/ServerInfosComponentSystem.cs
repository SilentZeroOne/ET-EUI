namespace ET
{
    public static class ServerInfosComponentSystem
    {
        public static void Add(this ServerInfosComponent self, ServerInfo value)
        {
            self.ServerInfoList.Add(value);
        }
    }

    public class ServerInfosComponentDestroySystem: DestroySystem<ServerInfosComponent>
    {
        public override void Destroy(ServerInfosComponent self)
        {
            foreach (var info in self.ServerInfoList)
            {
                info?.Dispose();
            }
            self.ServerInfoList.Clear();
            self.CurrentServerId = 0;
        }
    }
}