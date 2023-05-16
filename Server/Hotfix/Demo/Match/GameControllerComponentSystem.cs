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
        public static async void StartGame(this GameControllerComponent self)
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

            
            Log.Info($"Room {room.Id} 发牌结束");

            await TimerComponent.Instance.WaitAsync(1000);

            var firstPlayer = self.RandomFirstPlayer();
            Lo2C_CurrentPlayer lo2CCurrentPlayer = new Lo2C_CurrentPlayer()
            {
                ActionType = (int)ActionType.RobLandLord, UnitId = firstPlayer
            };

            room.GetComponent<OrderControllerComponent>().Init(firstPlayer);
            room.GetComponent<OrderControllerComponent>().CurrentPlayer = firstPlayer;
            room.Broadcast(lo2CCurrentPlayer);
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
        
        public static void CardsOnTable(this GameControllerComponent self, long unitId)
        {
            //往客户端发送地主牌信息
            var room = self.GetParent<Room>();
            var landLordCard = room.GetComponent<HandCardsComponent>();
            Lo2C_UpdateCardsInfo lordCardInfo = new Lo2C_UpdateCardsInfo();
            foreach (var card in landLordCard.Library)
            {
                lordCardInfo.CardsInfo.Add(card.ToMessage());
            }

            lordCardInfo.LordCard = 1;

            room.Broadcast(lordCardInfo);

            foreach (var player in room.Units)
            {
                Identify identify = unitId == player.Id? Identify.LandLord : Identify.Farmer;
                self.UpdateIdentify(player, identify);
            }
            
            //把地主牌加入地主手中
            lordCardInfo.LordCard = 0;
            var lordUnit = room.GetUnit(unitId);
            MessageHelper.SendToClient(lordUnit, lordCardInfo);

            var lordHandCard = lordUnit.GetComponent<HandCardsComponent>();
            while (landLordCard.Library.Count > 0)
            {
                var card = landLordCard.Library[^1];
                landLordCard.PopCard(card);
                lordHandCard.AddCard(card);
            }

            room.Broadcast(new Lo2C_UpdateIdentify() { UnitId = unitId, Identify = (int)Identify.LandLord });
            room.Broadcast(new Lo2C_CurrentPlayer() { ActionType = (int)ActionType.PlayCard, UnitId = unitId });
        }

        public static long RandomFirstPlayer(this GameControllerComponent self)
        {
            Room room = self.GetParent<Room>();
            var index = RandomHelper.RandomNumber(0, 2);

            return room.Units[index].Id;
        }

        public static void UpdateIdentify(this GameControllerComponent self, Unit unit, Identify identify)
        {
            unit.GetComponent<HandCardsComponent>().AccessIdentify = identify;
        }

        /// <summary>
        /// 回收所有卡牌到卡组
        /// </summary>
        /// <param name="self"></param>
        public static void BackToDeck(this GameControllerComponent self)
        {
            Room room = self.GetParent<Room>();
            DeckComponent deckComponent = room.GetComponent<DeckComponent>();

            foreach (var player in room.Units)
            {
                HandCardsComponent handCardsComponent = player.GetComponent<HandCardsComponent>();
                while (handCardsComponent.Library.Count > 0)
                {
                    var card = handCardsComponent.Library[^1];
                    handCardsComponent.PopCard(card);
                    deckComponent.AddCard(card);
                }
            }

            var roomCard = room.GetComponent<HandCardsComponent>();
            while (roomCard.Library.Count > 0)
            {
                var card = roomCard.Library[^1];
                roomCard.PopCard(card);
                deckComponent.AddCard(card);
            }
        }

    }
}