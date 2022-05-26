using System;
using System.Text.RegularExpressions;

namespace ET
{
    [FriendClass(typeof(Account))]
    public class C2A_LoginAccountHandler: AMRpcHandler<C2A_LoginAccount, A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的SceneType错误，当前scene为{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RepeatedRequestError;
                reply();
                session.Disconnect().Coroutine();
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

            if (!Regex.IsMatch(request.AccountName.Trim(), @"^(?=.*[0-9].*)(?=.*[a-z].*)(?=.*[A-Z].*).{6,15}$"))
            {
                response.Error = ErrorCode.ERR_AccountNameRegexError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            if (!Regex.IsMatch(request.AccountPassword.Trim(), @"^[A-Za-z0-9]+$"))
            {
                response.Error = ErrorCode.ERR_AccountPasswordRegexError;
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
                    }
                    else
                    {
                        account = session.AddChild<Account>();
                        account.AccountName = request.AccountName.Trim();
                        account.AccountPassword = request.AccountPassword;
                        account.CreateTime = TimeHelper.ServerNow();
                        account.AccountType = (int) AccountType.General;
                        await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save<Account>(account);
                    }

                    var startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "LoginCenter");
                    var loginCenterInstanceId = startSceneConfig.InstanceId;
                    var l2aResponse = (L2A_LoginAccountResponse) await ActorMessageSenderComponent.Instance.Call(loginCenterInstanceId,
                        new A2L_LoginAccountRequest() { AccountId = account.Id });

                    if (l2aResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = l2aResponse.Error;
                        reply();
                        session?.Disconnect().Coroutine();
                        account?.Dispose();
                        return;
                    }

                    long accountSessionInstanceid = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(account.Id);
                    Session otherSession = Game.EventSystem.Get(accountSessionInstanceid) as Session;
                    otherSession?.Send(new A2C_Disconnect(){Error = 0});
                    otherSession?.Disconnect().Coroutine();

                    session.DomainScene().GetComponent<AccountSessionsComponent>().Add(account.Id, session.InstanceId);
                    session.AddComponent<AccountCheckOutTimeComponent, long>(account.Id);
                    

                    string token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue).ToString();

                    var tokenComponent = session.DomainScene().GetComponent<TokenComponent>();
                    tokenComponent.Remove(account.Id);
                    tokenComponent.Add(account.Id, token);

                    response.Token = token;
                    response.AccountId = account.Id;

                    reply();
                    account?.Dispose();
                }
            }
        }
    }
}