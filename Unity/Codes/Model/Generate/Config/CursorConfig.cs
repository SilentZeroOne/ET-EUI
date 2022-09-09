using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class CursorConfigCategory : ProtoObject, IMerge
    {
        public static CursorConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, CursorConfig> dict = new Dictionary<int, CursorConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<CursorConfig> list = new List<CursorConfig>();
		
        public CursorConfigCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            CursorConfigCategory s = o as CursorConfigCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            foreach (CursorConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public CursorConfig Get(int id)
        {
            this.dict.TryGetValue(id, out CursorConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (CursorConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, CursorConfig> GetAll()
        {
            return this.dict;
        }

        public CursorConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class CursorConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>指针图片</summary>
		[ProtoMember(2)]
		public string CursorImage { get; set; }
		/// <summary>对应道具类型</summary>
		[ProtoMember(3)]
		public int[] CorrespondItemType { get; set; }

	}
}
