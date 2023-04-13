namespace ET
{
	public class Card: Entity, IAwake, IDestroy
	{
		/// <summary>
		/// 点数
		/// </summary>
		public int CardWeight;
		
		/// <summary>
		/// 花色
		/// </summary>
		public int CardSuit;
	}
}