using System;

namespace ET
{
    [Timer(TimerType.AccountSessionCheckOutTimer)]
    public class AccountCheckOutTimer: ATimer<AccountCheckOutTimeComponent>
    {
        public override void Run(AccountCheckOutTimeComponent self)
        {
            try
            {
                self.DeleteSession();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                throw;
            }
        }
    }

    public class AccountCheckOutTimeComponentAwakeSystem: AwakeSystem<AccountCheckOutTimeComponent,long>
    {
        public override void Awake(AccountCheckOutTimeComponent self,long accountId)
        {
            self.AccountId = accountId;
            TimerComponent.Instance.Remove(ref self.Timer);
            //10分钟超时就切断这个Account的连接
            self.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 60 * 1000 * 10, TimerType.AccountSessionCheckOutTimer, self);
        }
    }

    public class AccountCheckOutTimeComponentDestroySystem: DestroySystem<AccountCheckOutTimeComponent>
    {
        public override void Destroy(AccountCheckOutTimeComponent self)
        {
            self.AccountId = 0;
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }

    [FriendClass(typeof (AccountCheckOutTimeComponent))]
    public static class AccountCheckOutTimeComponentSystem
    {
        public static void DeleteSession(this AccountCheckOutTimeComponent self)
        {
            Session session = self.Parent as Session;
            var accountSessions = session?.DomainScene().GetComponent<AccountSessionsComponent>();
            var sessionInstanceId = accountSessions.Get(self.AccountId);
            if (session?.InstanceId == sessionInstanceId)
            {
                accountSessions.Remove(self.AccountId);
            }
            session?.Send(new A2C_Disconnect(){Error = ErrorCode.ERR_AccountOverTimeError});
            session?.Disconnect().Coroutine();
        }
    }
}