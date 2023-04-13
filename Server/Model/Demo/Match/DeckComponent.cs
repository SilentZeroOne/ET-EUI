﻿using System.Collections.Generic;

namespace ET
{
	[ComponentOf()]
	public class DeckComponent: Entity, IAwake, IDestroy
	{
		public readonly List<Card> CardLibrary = new List<Card>();

		public int CardsCount => this.CardLibrary.Count;
	}
}