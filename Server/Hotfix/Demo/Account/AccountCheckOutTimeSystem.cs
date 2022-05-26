using System;

namespace ET
{
    [Timer(TimerType.AccountSessionCheckOutTimer)]
    public class AccountSessionCheckOutTimer : ATimer<AccountCheckOutTimeComponent>
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

    public class AccountCheckOutTimeAwakeSystem: AwakeSystem<AccountCheckOutTimeComponent,long>
    {
        public override void Awake(AccountCheckOutTimeComponent self, long accountId)
        {
            self.AccountId = accountId;
            TimerComponent.Instance.Remove(ref self.Timer);
            self.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 60 * 1000 * 10, TimerType.AccountSessionCheckOutTimer, self);
        }
    }
    
    public class AccountCheckOutTimeDestorySystem: DestroySystem<AccountCheckOutTimeComponent>
    {
        public override void Destroy(AccountCheckOutTimeComponent self)
        {
            self.AccountId = 0;
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }

    [FriendClass(typeof(AccountCheckOutTimeComponent))]
    public static class AccountCheckOutTimeSystem
    {
        public static void DeleteSession(this AccountCheckOutTimeComponent self)
        {
            Session session = self.Parent.GetComponent<Session>();

            var accountSessions = session?.DomainScene().GetComponent<AccountSessionsComponent>();
            var sessionInstanceId = accountSessions.Get(self.AccountId);
            if (session?.InstanceId == sessionInstanceId)
            {
                accountSessions.Remove(self.AccountId);
            }

            session?.Send(new A2C_Disconnect() { Error = 1 });
            session?.Disconnect().Coroutine();
        }
    }
}