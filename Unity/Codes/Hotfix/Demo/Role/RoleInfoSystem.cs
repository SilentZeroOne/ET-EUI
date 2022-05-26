namespace ET
{
    [FriendClass(typeof(RoleInfo))]
    public static class RoleInfoSystem
    {
        public static void FromMessage(this RoleInfo self, RoleInfoProto message)
        {
            self.Id = message.Id;
            self.Name = message.Name;
            self.State = message.State;
            self.AccountId = message.AccountId;
            self.CreateTime = message.CreateTime;
            self.LastLoginTime = message.LastLoginTime;
            self.ServerId = message.ServerId;
        }

        public static RoleInfoProto ToMessage(this RoleInfo self)
        {
            return new RoleInfoProto()
            {
                AccountId = self.AccountId,
                CreateTime = self.CreateTime,
                Id = self.Id,
                LastLoginTime = self.LastLoginTime,
                Name = self.Name,
                ServerId = self.ServerId,
                State = self.State
            };
        }
    }
}