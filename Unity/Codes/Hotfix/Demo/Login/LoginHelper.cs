using System;


namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2C_LoginAccount a2CLoginAccount = null;
            Session accountSession = null;

            try
            {
                accountSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                password = MD5Helper.StringMD5(password);
                a2CLoginAccount =
                        (A2C_LoginAccount) await accountSession.Call(new C2A_LoginAccount() { AccountName = account, AccountPassword = password });
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
                return a2CLoginAccount.Error;
            }

            zoneScene.GetComponent<SessionComponent>().Session = accountSession;//保持连接
            zoneScene.GetComponent<SessionComponent>().Session.AddComponent<PingComponent>();
            
            var accountInfo = zoneScene.GetComponent<AccountInfoComponent>();
            accountInfo.Token = a2CLoginAccount.Token;
            accountInfo.AccountId = a2CLoginAccount.AccountId;

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetServerInfo(Scene zoneScene)
        {
            A2C_GetServerInfo infos = null;

            try
            {
                infos = (A2C_GetServerInfo) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetServerInfo()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            if (infos.Error != ErrorCode.ERR_Success)
            {
                return infos.Error;
            }

            foreach (var serverInfo in infos.ServerInfoList)
            {
                var info = zoneScene.GetComponent<ServerInfosComponent>().AddChild<ServerInfo>();
                info.FromMessage(serverInfo);
                zoneScene.GetComponent<ServerInfosComponent>().Add(info);
            }
            
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
        
    }
}