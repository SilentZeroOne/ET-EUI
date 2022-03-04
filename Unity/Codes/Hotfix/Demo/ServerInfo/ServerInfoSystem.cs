namespace ET
{
    public static class ServerInfoSystem
    {
        public static void FromMessage(this ServerInfo self, ServerInfoProto message)
        {
            self.Id = message.Id;
            self.Status = message.Status;
            self.ServerName = message.Name;
        }

        public static ServerInfoProto ToMessage(this ServerInfo self)
        {
            return new ServerInfoProto() { Id = (int) self.Id, Name = self.ServerName, Status = self.Status };
        }
    }
}