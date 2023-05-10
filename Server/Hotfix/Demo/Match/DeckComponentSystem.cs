using System.Collections.Generic;

namespace ET
{
	public class DeckComponentAwakeSystem: AwakeSystem<DeckComponent>
	{
		public override void Awake(DeckComponent self)
		{
			self.Awake();
		}
	}

	public class DeckComponentDestroySystem: DestroySystem<DeckComponent>
	{
		public override void Destroy(DeckComponent self)
		{

		}
	}

	[FriendClass(typeof (DeckComponent))]
	public static class DeckComponentSystem
	{
		public static void Awake(this DeckComponent self)
		{
			self.CreateDeck();
			self.Shuffle();
		}

		/// <summary>
		/// 创建一副牌
		/// </summary>
		/// <param name="self"></param>
		public static void CreateDeck(this DeckComponent self)
		{
			for (int color = 0; color < 4; color++)
			{
				for (int value = 0; value < 13; value++)
				{
					self.CardLibrary.Add(CardFactory.CreateCard(self, value, color));
				}
			}

			//创建大小王
			self.CardLibrary.Add(CardFactory.CreateCard(self, Weight.LJoker, Suits.None));
			self.CardLibrary.Add(CardFactory.CreateCard(self, Weight.SJoker, Suits.None));
		}

		/// <summary>
		/// 添加牌
		/// </summary>
		/// <param name="self"></param>
		/// <param name="card"></param>
		public static void AddCard(this DeckComponent self, Card card)
		{
			self.CardLibrary.Add(card);
			self.AddChild(card);
		}

		/// <summary>
		/// 发牌
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Card DealCard(this DeckComponent self)
		{
			var card = self.CardLibrary[self.CardsCount - 1];
			self.CardLibrary.Remove(card);

			return card;
		}

		/// <summary>
		/// 洗牌
		/// </summary>
		/// <param name="self"></param>
		public static void Shuffle(this DeckComponent self)
		{
			for (int i = 0; i < self.CardsCount; i++)
			{
				int j = RandomHelper.RandomNumber(i, self.CardsCount);
				(self.CardLibrary[i], self.CardLibrary[j]) = (self.CardLibrary[j], self.CardLibrary[i]);
			}
		}
		
	}
}