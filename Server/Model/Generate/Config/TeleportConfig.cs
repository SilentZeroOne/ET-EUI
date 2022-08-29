using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class TeleportConfigCategory : ProtoObject, IMerge
    {
        public static TeleportConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, TeleportConfig> dict = new Dictionary<int, TeleportConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<TeleportConfig> list = new List<TeleportConfig>();
		
        public TeleportConfigCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            TeleportConfigCategory s = o as TeleportConfigCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            foreach (TeleportConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public TeleportConfig Get(int id)
        {
            this.dict.TryGetValue(id, out TeleportConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (TeleportConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, TeleportConfig> GetAll()
        {
            return this.dict;
        }

        public TeleportConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class TeleportConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>场景名</summary>
		[ProtoMember(2)]
		public string SceneName { get; set; }
		/// <summary>传送点名称</summary>
		[ProtoMember(3)]
		public string PointName { get; set; }
		/// <summary>目标场景名</summary>
		[ProtoMember(4)]
		public string TargetSceneName { get; set; }
		/// <summary>目标位置X</summary>
		[ProtoMember(5)]
		public float TargetPosX { get; set; }
		/// <summary>目标位置Y</summary>
		[ProtoMember(6)]
		public float TargetPosY { get; set; }
		/// <summary>目标位置Z</summary>
		[ProtoMember(7)]
		public float TargetPosZ { get; set; }

	}
}
