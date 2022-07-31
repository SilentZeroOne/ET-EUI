using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class GameSceneConfigCategory : ProtoObject, IMerge
    {
        public static GameSceneConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, GameSceneConfig> dict = new Dictionary<int, GameSceneConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<GameSceneConfig> list = new List<GameSceneConfig>();
		
        public GameSceneConfigCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            GameSceneConfigCategory s = o as GameSceneConfigCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            foreach (GameSceneConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public GameSceneConfig Get(int id)
        {
            this.dict.TryGetValue(id, out GameSceneConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (GameSceneConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, GameSceneConfig> GetAll()
        {
            return this.dict;
        }

        public GameSceneConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class GameSceneConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>场景名字</summary>
		[ProtoMember(2)]
		public string SceneName { get; set; }

	}
}
