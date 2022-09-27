using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class CropConfigCategory : ProtoObject, IMerge
    {
        public static CropConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, CropConfig> dict = new Dictionary<int, CropConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<CropConfig> list = new List<CropConfig>();
		
        public CropConfigCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            CropConfigCategory s = o as CropConfigCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            foreach (CropConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public CropConfig Get(int id)
        {
            this.dict.TryGetValue(id, out CropConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (CropConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, CropConfig> GetAll()
        {
            return this.dict;
        }

        public CropConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class CropConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>成长天数</summary>
		[ProtoMember(2)]
		public int[] GrowthDays { get; set; }
		/// <summary>总成长天数</summary>
		[ProtoMember(3)]
		public int TotalGrowthDays { get; set; }
		/// <summary>不同阶段物品prefab</summary>
		[ProtoMember(4)]
		public string[] GrowthPrefabs { get; set; }
		/// <summary>成长阶段图片</summary>
		[ProtoMember(5)]
		public string[] GrowthSprites { get; set; }
		/// <summary>可种植季节</summary>
		[ProtoMember(6)]
		public int[] Seasons { get; set; }
		/// <summary>收割工具</summary>
		[ProtoMember(7)]
		public int[] HarvestToolItemIDs { get; set; }
		/// <summary>每种工具使用次数</summary>
		[ProtoMember(8)]
		public int[] RequireActionCount { get; set; }
		/// <summary>转换新物品ID</summary>
		[ProtoMember(9)]
		public int TransferItemId { get; set; }
		/// <summary>果实ID</summary>
		[ProtoMember(10)]
		public int[] ProducedItemIDs { get; set; }
		/// <summary>果实最小数量</summary>
		[ProtoMember(11)]
		public int[] ProducedItemMinAmount { get; set; }
		/// <summary>果实最大数量</summary>
		[ProtoMember(12)]
		public int[] ProducedItemMaxAmount { get; set; }
		/// <summary>果实生成范围</summary>
		[ProtoMember(13)]
		public int[] SpawnRadius { get; set; }
		/// <summary>再次生长时间</summary>
		[ProtoMember(14)]
		public int DaysToRegrow { get; set; }
		/// <summary>再次生长次数</summary>
		[ProtoMember(15)]
		public int RegrowTimes { get; set; }
		/// <summary>是否生成在玩家位置</summary>
		[ProtoMember(16)]
		public int GenerateAtPlayerPos { get; set; }
		/// <summary>是否有动画</summary>
		[ProtoMember(17)]
		public int HasAnimation { get; set; }
		/// <summary>是否有特效</summary>
		[ProtoMember(18)]
		public int HasParticleEffect { get; set; }
		/// <summary>是否能消失</summary>
		[ProtoMember(19)]
		public int CanFade { get; set; }
		/// <summary>是否消失父物体</summary>
		[ProtoMember(20)]
		public int FadeParent { get; set; }
		/// <summary>特效Prefab</summary>
		[ProtoMember(21)]
		public string ParticlePrefab { get; set; }
		/// <summary>特效生成位置</summary>
		[ProtoMember(22)]
		public int[] ParticlePos { get; set; }

	}
}
