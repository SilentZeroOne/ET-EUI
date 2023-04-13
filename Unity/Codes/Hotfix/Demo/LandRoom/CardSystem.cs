namespace ET
{
	public class CardAwakeSystem: AwakeSystem<Card>
	{
		public override void Awake(Card self)
		{

		}
	}

	public class CardDestroySystem: DestroySystem<Card>
	{
		public override void Destroy(Card self)
		{

		}
	}

	[FriendClass(typeof (Card))]
	public static class CardSystem
	{
		public static bool Equals(this Card self, Card other)
		{
			return self.CardWeight == other.CardWeight && self.CardSuit == other.CardSuit;
		}
		
		public static string GetName(this Card self)
		{
			return $"{(Suits)self.CardSuit} {(Weight)self.CardWeight}";
		}

		public static void FromMessage(this Card self, CardInfo proto)
		{
			self.CardSuit = proto.CardSuit;
			self.CardWeight = proto.CardWeight;
		}

		public static CardInfo ToMessage(this Card self)
		{
			return new CardInfo() { CardWeight = self.CardWeight, CardSuit = self.CardSuit };
		}
	}
}