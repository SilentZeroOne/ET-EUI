using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    public class C2A_CreateRoleHandler : AMRpcHandler<C2A_CreateRole, A2C_CreateRole>
    {
        protected override async ETTask Run(Session session, C2A_CreateRole request, A2C_CreateRole response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{session.DomainScene().SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                session.Dispose();
                return;
            }

            // 防止重复请求
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RepeatedRequestError;
                reply();
                return;
            }

            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (string.IsNullOrEmpty(token) || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session?.Disconnect().Coroutine();
                return;
            }

            if (string.IsNullOrEmpty(request.NickName))
            {
                response.Error = ErrorCode.ERR_RoleNameIsNull;
                reply();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRole, request.AccountId))
            {
                var roleInfos = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                        .Query<RoleInfo>(r => r.AccountId == request.AccountId && r.State != (int)RoleInfoState.Freeze);
                if (roleInfos != null && roleInfos.Count > 0)
                {
                    response.Error = ErrorCode.ERR_RoleAlreadyExist;
                    reply();
                    return;
                }

                RoleInfo roleInfo = session.AddChildWithId<RoleInfo>(IdGenerater.Instance.GenerateUnitId(session.DomainZone()));
                roleInfo.NickName = request.NickName;
                roleInfo.AccountId = request.AccountId;
                roleInfo.State = (int)RoleInfoState.Normal;
                roleInfo.CreateTime = TimeHelper.ServerNow();
                roleInfo.LastLoginTime = TimeHelper.ServerNow();

                await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save(roleInfo);

                response.RoleInfo = roleInfo.ToMessage();
                reply();
                
                roleInfo?.Dispose();
            }
        }
    }
}