namespace ET
{
    public class RankInfoAwakeSystem: AwakeSystem<RankInfo>
    {
        public override void Awake(RankInfo self)
        {

        }
    }

    public class RankInfoDestroySystem: DestroySystem<RankInfo>
    {
        public override void Destroy(RankInfo self)
        {
            self.Name = null;
            self.Count = 0;
            self.UnitId = 0;
        }
    }
    

    [FriendClass(typeof (RankInfo))]
    public static class RankInfoSystem
    {
        public static void FromMessage(this RankInfo self, RankInfoProto proto)
        {
            self.Id = proto.Id;
            self.Count = proto.Count;
            self.Name = proto.Name;
            self.UnitId = proto.UnitId;
        }

        public static RankInfoProto ToMessage(this RankInfo self)
        {
            return new RankInfoProto() { Count = self.Count, Id = self.Id, Name = self.Name, UnitId = self.UnitId };
        }
        
    }
}