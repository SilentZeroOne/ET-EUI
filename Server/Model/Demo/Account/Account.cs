namespace ET
{
    public enum AccountType
    {
        Admin,
        General,
        BlackList
    }
    
    public class Account: Entity, IAwake
    {
        public string AccountName;

        public string AccountPassword;

        public long CreateTime;

        public int AccountType;
    }
}