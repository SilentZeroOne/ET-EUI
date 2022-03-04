using System;

namespace ET
{
    public class C2A_GetServerInfoHandler : AMRpcHandler<C2A_GetServerInfo,A2C_GetServerInfo>
    {
        protected override async ETTask Run(Session session, C2A_GetServerInfo request, A2C_GetServerInfo response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误，当前Scene为{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            var token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session?.Disconnect().Coroutine();
                return;
            }

            foreach (var serverInfo in session.DomainScene().GetComponent<ServerInfoManagerComponent>().ServerInfos)
            {
                response.ServerInfoList.Add(serverInfo.ToMessage());
            }

            reply();
            
            await ETTask.CompletedTask;
        }
    }
}