using System;

namespace ET
{
    public class C2A_CreateRoleHandler : AMRpcHandler<C2A_CrerateRole,A2C_CreateRole>
    {
        protected override async ETTask Run(Session session, C2A_CrerateRole request, A2C_CreateRole response, Action reply)
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

            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.Err_RoleNameIsNull;
                reply();
                return;
            }


            await ETTask.CompletedTask;
        }
    }
}