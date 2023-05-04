namespace ET
{
	public class GameControllerComponentAwakeSystem: AwakeSystem<GameControllerComponent>
	{
		public override void Awake(GameControllerComponent self)
		{

		}
	}

	public class GameControllerComponentDestroySystem: DestroySystem<GameControllerComponent>
	{
		public override void Destroy(GameControllerComponent self)
		{

		}
	}

    [FriendClass(typeof(GameControllerComponent))]
    [FriendClassAttribute(typeof(ET.Room))]
    [FriendClassAttribute(typeof(ET.HandCardsComponent))]
    public static class GameControllerComponentSystem
    {
        public static void StartGame(this GameControllerComponent self)
        {
            Room room = self.GetParent<Room>();
            room.GetComponent<HandCardsComponent>().Reset();
            Unit[] units = room.Units;

            foreach (var unit in units)
            {
                if (unit.GetComponent<HandCardsComponent>() == null)
                    unit.AddComponent<HandCardsComponent>();
                unit.GetComponent<HandCardsComponent>().Reset();
            }
            
            self.DealCards();

            foreach (var unit in units)
            {
                var handCards = unit.GetComponent<HandCardsComponent>();

                Lo2C_UpdateCardsInfo updateCardsInfo = new Lo2C_UpdateCardsInfo();
                foreach (var card in handCards.Library)
                {
                    updateCardsInfo.CardsInfo.Add(card.ToMessage());
                }

                updateCardsInfo.LordCard = 0;

                MessageHelper.SendToClient(unit, updateCardsInfo);
            }

            var landLordCard = room.GetComponent<HandCardsComponent>();
            Lo2C_UpdateCardsInfo lordCardInfo = new Lo2C_UpdateCardsInfo();
            foreach (var card in landLordCard.Library)
            {
                lordCardInfo.CardsInfo.Add(card.ToMessage());
            }

            lordCardInfo.LordCard = 1;

            room.Broadcast(lordCardInfo);
            
            Log.Info($"Room {room.Id} 开始游戏");
        }

        /// <summary>
        /// 轮流发牌
        /// </summary>
        /// <param name="self"></param>
        public static void DealCards(this GameControllerComponent self)
        {
            Room room = self.GetParent<Room>();
            Unit[] units = room.Units;

            int index = 0;
            //一共先发51张牌
            for (int i = 0; i < 51; i++)
            {
                if (index == 3)
                {
                    index = 0;
                }

                self.DealTo(units[index].InstanceId);

                index++;
            }

            //三张地主牌发给room
            for (int i = 0; i < 3; i++)
            {
                self.DealTo(room.InstanceId);
            }
        }

        public static void DealTo(this GameControllerComponent self, long instanceId)
        {
            Room room = self.GetParent<Room>();
            Card card = room.GetComponent<DeckComponent>().DealCard();
            Entity entity = EventSystem.Instance.Get(instanceId);

            entity?.GetComponent<HandCardsComponent>()?.AddCard(card);
        }

    }
}