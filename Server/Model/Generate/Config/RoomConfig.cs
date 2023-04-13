using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class RoomConfigCategory : ProtoObject, IMerge
    {
        public static RoomConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, RoomConfig> dict = new Dictionary<int, RoomConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<RoomConfig> list = new List<RoomConfig>();
		
        public RoomConfigCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            RoomConfigCategory s = o as RoomConfigCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            foreach (RoomConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public RoomConfig Get(int id)
        {
            this.dict.TryGetValue(id, out RoomConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (RoomConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, RoomConfig> GetAll()
        {
            return this.dict;
        }

        public RoomConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class RoomConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>倍率</summary>
		[ProtoMember(2)]
		public int Multiples { get; set; }
		/// <summary>基础分</summary>
		[ProtoMember(3)]
		public int BasePointPerMatch { get; set; }
		/// <summary>房间最低门槛</summary>
		[ProtoMember(4)]
		public int MinThreshold { get; set; }

	}
}
