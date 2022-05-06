using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class ServerInfosConfigCategory : ProtoObject, IMerge
    {
        public static ServerInfosConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, ServerInfosConfig> dict = new Dictionary<int, ServerInfosConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<ServerInfosConfig> list = new List<ServerInfosConfig>();
		
        public ServerInfosConfigCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            ServerInfosConfigCategory s = o as ServerInfosConfigCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            foreach (ServerInfosConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public ServerInfosConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ServerInfosConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ServerInfosConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, ServerInfosConfig> GetAll()
        {
            return this.dict;
        }

        public ServerInfosConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class ServerInfosConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>服务器名字</summary>
		[ProtoMember(2)]
		public string ServerName { get; set; }

	}
}
