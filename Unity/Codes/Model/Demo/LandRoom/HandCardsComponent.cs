using System.Collections.Generic;

namespace ET
{
	[ComponentOf()]
	[ChildType()]
	public class HandCardsComponent: Entity, IAwake, IDestroy
	{
		//手牌
		public readonly List<Card> Library = new List<Card>();
		
		//身份
		public Identify AccessIdentify { get; set; }

		public int CardsCount => this.Library.Count;
	}
}