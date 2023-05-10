using System.Collections.Generic;

namespace ET
{
	[ComponentOf()]
	public class OrderControllerComponent : Entity,IAwake,IDestroy
	{
		//起手玩家的抢地主情况
		public KeyValuePair<long,bool> FirstAuthority { get; set; }

		//所有玩家的抢地主情况
		public Dictionary<long, bool> GameLordState = new Dictionary<long, bool>(3);
		
		//本轮出牌最大玩家
		public long Biggest { get; set; }
		
		//当前出牌玩家
		public long CurrentPlayer { get; set; }
		
		//当前抢地主玩家
		public int SelectLordIndex { get; set; } = 1;
	}
}