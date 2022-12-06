namespace ET
{
    [FriendClass(typeof (RoleInfo))]
    public static class RoleInfoSystem
    {
        public static void FromMessage(this RoleInfo self, RoleInfoProto proto)
        {
            self.Id = proto.Id;
            self.State = proto.State;
            self.AccountId = proto.AccountId;
            self.CreateTime = proto.CreateTime;
            self.NickName = proto.NickName;
            self.LastLoginTime = proto.LastLoginTime;
        }

        public static RoleInfoProto ToMessage(this RoleInfo self)
        {
            return new RoleInfoProto()
            {
                Id = self.Id,
                State = self.State,
                AccountId = self.AccountId,
                CreateTime = self.CreateTime,
                NickName = self.NickName,
                LastLoginTime = self.LastLoginTime
            };
        }
    }
}