
using System;
using System.Text.RegularExpressions;

namespace ET
{
    [FriendClassAttribute(typeof(ET.Account))]
    public class C2A_RegisterAccountHandler : AMRpcHandler<C2A_RegisterAccount, A2C_RegisterAccount>
    {
        protected override async ETTask Run(Session session, C2A_RegisterAccount request, A2C_RegisterAccount response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{session.DomainScene().SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                session.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RepeatedRequestError;
                reply();
                return;
            }

            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            if (string.IsNullOrEmpty(request.AccountName) || string.IsNullOrEmpty(request.AccountPassword))
            {
                response.Error = ErrorCode.ERR_LoginInfoError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            if (!Regex.IsMatch(request.AccountName.Trim(), @"^(?=.*[a-z].*)(?=.*[A-Z].*).{4,15}$"))
            {
                response.Error = ErrorCode.ERR_AccountNameRegexError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.AccountName.Trim().GetHashCode()))
                {
                    var accountList = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                            .Query<Account>(d => d.AccountName == request.AccountName.Trim());

                    Account account = null;
                    
                    if (accountList != null && accountList.Count > 0)
                    {
                        response.Error = ErrorCode.ERR_AccountAlreadyRegisteredError;
                        reply();
                        session.Disconnect().Coroutine();
                    }
                    else
                    {
                        account = session.AddChild<Account>();
                        account.AccountName = request.AccountName.Trim();
                        account.AccountPassword = request.AccountPassword;
                        account.AccountType = (int)AccountType.Normal;
                        account.CreateTime = TimeHelper.ServerNow();
                        await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save(account);

                        reply();
                        account?.Dispose();
                    }
                }
            }
        }
    }
}