using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class AnimatorControllerConfigCategory : ProtoObject, IMerge
    {
        public static AnimatorControllerConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, AnimatorControllerConfig> dict = new Dictionary<int, AnimatorControllerConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<AnimatorControllerConfig> list = new List<AnimatorControllerConfig>();
		
        public AnimatorControllerConfigCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            AnimatorControllerConfigCategory s = o as AnimatorControllerConfigCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            foreach (AnimatorControllerConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public AnimatorControllerConfig Get(int id)
        {
            this.dict.TryGetValue(id, out AnimatorControllerConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (AnimatorControllerConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, AnimatorControllerConfig> GetAll()
        {
            return this.dict;
        }

        public AnimatorControllerConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class AnimatorControllerConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>动画控制器名字</summary>
		[ProtoMember(2)]
		public string AnimatorName { get; set; }
		/// <summary>状态</summary>
		[ProtoMember(3)]
		public int AniamtorStatus { get; set; }
		/// <summary>覆盖的动画控制器名字</summary>
		[ProtoMember(4)]
		public string OverrideControllerName { get; set; }

	}
}
