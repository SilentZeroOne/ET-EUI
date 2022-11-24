using System;


namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountInfoComponent))]
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2C_LoginAccount a2CLoginAccount = null;
            Session accountSession = null;
            bool alreadyConnected = false;

            try
            {
                var session = zoneScene.GetComponent<SessionComponent>().Session;
                if (session != null)
                {
                    alreadyConnected = true;
                }

                accountSession = alreadyConnected? session : zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                
                password = MD5Helper.StringMD5(password);
                a2CLoginAccount =
                        (A2C_LoginAccount)await accountSession.Call(new C2A_LoginAccount() { AccountName = account, AccountPassword = password });
            }
            catch (Exception e)
            {
                accountSession?.Dispose();
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            if (a2CLoginAccount.Error != ErrorCode.ERR_Success)
            {
                accountSession?.Dispose();
                Log.Error(a2CLoginAccount.Error.ToString());
                return a2CLoginAccount.Error;
            }

            if (!alreadyConnected)
            {
                zoneScene.GetComponent<SessionComponent>().Session = accountSession;//保持连接
                zoneScene.GetComponent<SessionComponent>().Session.AddComponent<PingComponent>();
            }

            var accountInfo = zoneScene.GetComponent<AccountInfoComponent>();
            accountInfo.Token = a2CLoginAccount.Token;
            accountInfo.AccountId = a2CLoginAccount.AccountId;

            Log.Debug($"Login Account! {accountInfo.Token} {accountInfo.AccountId}");
            
            return ErrorCode.ERR_Success;
        }
        
        public static async ETTask<int> Register(Scene zoneScene, string address, string account, string password)
        {
            A2C_RegisterAccount a2CRegisterAccount = null;
            Session accountSession = null;

            try
            {
                accountSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                password = MD5Helper.StringMD5(password);
                a2CRegisterAccount =
                        (A2C_RegisterAccount)await accountSession.Call(new C2A_RegisterAccount() { AccountName = account, AccountPassword = password });
            }
            catch (Exception e)
            {
                accountSession?.Dispose();
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            if (a2CRegisterAccount.Error != ErrorCode.ERR_Success)
            {
                accountSession?.Dispose();
                Log.Error(a2CRegisterAccount.Error.ToString());
                return a2CRegisterAccount.Error;
            }

            zoneScene.GetComponent<SessionComponent>().Session = accountSession;//保持连接
            zoneScene.GetComponent<SessionComponent>().Session.AddComponent<PingComponent>();
            
            Log.Debug($"Register account success!");
            
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRealm(Scene zoneScene)
        {
            A2C_GetRealm a2CGetRealm = null;
            Session accountSession = zoneScene.GetComponent<SessionComponent>().Session;

            try
            {
                var accountInfo = zoneScene.GetComponent<AccountInfoComponent>();
                a2CGetRealm = (A2C_GetRealm)await accountSession.Call(new C2A_GetRealm()
                {
                    AccountId = accountInfo.AccountId, Token = accountInfo.Token
                });

                if (a2CGetRealm.Error != ErrorCode.ERR_Success)
                {
                    Log.Error(a2CGetRealm.Error.ToString());
                    return a2CGetRealm.Error;
                }

                accountInfo.RealmAddress = a2CGetRealm.RealmAddress;
                accountInfo.RealmKey = a2CGetRealm.RealmKey;

                accountSession?.Dispose();//获得realm服务器消息后 断开与Account服务器的连接
                
                return ErrorCode.ERR_Success;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }
        }
    }
}