namespace ET
{
    public enum AccountType
    {
        BlackList = 0,
        Normal = 1,
        Admin = 2,
    }
    
    public class Account: Entity, IAwake, IDestroy
    {
        public string AccountName;
        public string AccountPassword;
        public long CreateTime;
        public int AccountType;
    }
}