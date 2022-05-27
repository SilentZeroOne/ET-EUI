using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class BattleLevelConfigCategory : ProtoObject, IMerge
    {
        public static BattleLevelConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, BattleLevelConfig> dict = new Dictionary<int, BattleLevelConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<BattleLevelConfig> list = new List<BattleLevelConfig>();
		
        public BattleLevelConfigCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            BattleLevelConfigCategory s = o as BattleLevelConfigCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            foreach (BattleLevelConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public BattleLevelConfig Get(int id)
        {
            this.dict.TryGetValue(id, out BattleLevelConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (BattleLevelConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, BattleLevelConfig> GetAll()
        {
            return this.dict;
        }

        public BattleLevelConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class BattleLevelConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>所属ai</summary>
		[ProtoMember(2)]
		public int AIConfigId { get; set; }
		/// <summary>此ai中的顺序</summary>
		[ProtoMember(3)]
		public int Order { get; set; }
		/// <summary>节点名字</summary>
		[ProtoMember(4)]
		public string Name { get; set; }
		/// <summary>节点参数</summary>
		[ProtoMember(5)]
		public int[] NodeParams { get; set; }
		/// <summary>怪物列表</summary>
		[ProtoMember(6)]
		public int[] MonsterIds { get; set; }
		/// <summary>怪物数量</summary>
		[ProtoMember(7)]
		public int[] MonsterCount { get; set; }
		/// <summary>准入等级范围</summary>
		[ProtoMember(8)]
		public int[] MinEnterLevel { get; set; }

	}
}
