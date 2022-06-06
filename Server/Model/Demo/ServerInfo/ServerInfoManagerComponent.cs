using System.Collections.Generic;

namespace ET
{
    [ChildType(typeof(ServerInfo))]
    [ComponentOf()]
    public class ServerInfoManagerComponent : Entity,IAwake,IDestroy,ILoad
    {
        public List<ServerInfo> ServerInfos = new List<ServerInfo>();
    }
}