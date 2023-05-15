namespace ET
{
	public class HandCardsComponentAwakeSystem: AwakeSystem<HandCardsComponent>
	{
		public override void Awake(HandCardsComponent self)
		{
		}
	}

	public class HandCardsComponentDestroySystem: DestroySystem<HandCardsComponent>
	{
		public override void Destroy(HandCardsComponent self)
		{
			self.Reset();
		}
	}

	[FriendClass(typeof (HandCardsComponent))]
	public static class HandCardsComponentSystem
	{
		public static Card[] GetAllCards(this HandCardsComponent self)
		{
			self.Sort();
			return self.Library.ToArray();
		}

		public static void Sort(this HandCardsComponent self)
		{
			self.Library.Sort();
		}

		public static void AddCard(this HandCardsComponent self, Card card)
		{
			self.Library.Add(card);
			self.AddChild(card);
		}

		public static void PopCard(this HandCardsComponent self, Card card)
		{
			self.Library.Remove(card);
		}

		public static void Reset(this HandCardsComponent self)
		{
			foreach (var card in self.Library)
			{
				card.Dispose();
			}
			self.Library.Clear();
			self.AccessIdentify = Identify.None;
		}

		public static int GetCardIndex(this HandCardsComponent self, Card card)
		{
			return self.Library.IndexOf(card);
		}
	}
}