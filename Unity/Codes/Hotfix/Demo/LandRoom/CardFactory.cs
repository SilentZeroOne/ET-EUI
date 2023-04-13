namespace ET
{
    [FriendClassAttribute(typeof(ET.Card))]
    public static class CardFactory
    {
        public static Card CreateCard(Entity parent, int weight, int suit)
        {
            var card = parent.AddChild<Card>();
            card.CardWeight = weight;
            card.CardSuit = suit;

            return card;
        }

        public static Card CreateCard(Entity parent, Weight weight, Suits suit)
        {
            var card = parent.AddChild<Card>();
            card.CardWeight = (int)weight;
            card.CardSuit = (int)suit;

            return card;
        }
    }
}