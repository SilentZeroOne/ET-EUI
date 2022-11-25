using System;
using System.Text.RegularExpressions;

namespace ET
{
    [FriendClassAttribute(typeof(ET.Account))]
    public class C2A_LoginAccountHandler : AMRpcHandler<C2A_LoginAccount, A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
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
                        account = accountList[0];
                        session.AddChild(account);
                        
                        if (account.AccountType == (int) AccountType.BlackList)
                        {
                            response.Error = ErrorCode.ERR_BlacklistError;
                            reply();
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }

                        if (account.AccountPassword != request.AccountPassword)
                        {
                            response.Error = ErrorCode.ERR_AccountPasswordError;
                            reply();
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }

                        //TODO 确认Gate上有没有已经存在的Player playerofflinetimeoutComponent相关
                        
                        long sessionInstanceId = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(account.Id);
                        Session otherSession = Game.EventSystem.Get(sessionInstanceId) as Session;
                        otherSession?.Send(new A2C_Disconnect(){Error = ErrorCode.ERR_OtherAccountLogin});
                        otherSession?.Disconnect().Coroutine();

                        session.DomainScene().GetComponent<AccountSessionsComponent>().Add(account.Id, session.InstanceId);
                        session.AddComponent<AccountCheckOutTimeComponent, long>(account.Id);

                        string token = TimeHelper.ServerNow() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue).ToString();
                        var tokenComponent = session.DomainScene().GetComponent<TokenComponent>();
                        tokenComponent.Remove(account.Id);
                        tokenComponent.Add(account.Id, token);

                        response.Token = token;
                        response.AccountId = account.Id;
                        reply();
                        
                        account?.Dispose();
                    }
                    else
                    {
                        response.Error = ErrorCode.ERR_NoSuchAccountError;
                        reply();
                        session.Disconnect().Coroutine();
                    }
                }
            }
        }
    }
}