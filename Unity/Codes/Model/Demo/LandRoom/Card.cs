using System;

namespace ET
{
	[EnableMethod]
	public class Card: Entity, IAwake, IDestroy,IComparable
	{
		/// <summary>
		/// 点数
		/// </summary>
		public int CardWeight;
		
		/// <summary>
		/// 花色
		/// </summary>
		public int CardSuit;

		public int CompareTo(object obj)
		{
			Card other = obj as Card;
			var selfWeight = this.CardWeight;
			if (other == null) return 1;
			if (selfWeight > other.CardWeight) return 1;
			return -1;
		}
	}
}