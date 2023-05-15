using System;
using System.Collections.Generic;
using ET.EventType;

namespace ET
{
    [FriendClassAttribute(typeof(ET.Card))]
    [FriendClassAttribute(typeof(ET.HandCardsComponent))]
    public class Lo2C_UpdateCardsInfoHandler : AMHandler<Lo2C_UpdateCardsInfo>
    {
        protected override async void Run(Session session, Lo2C_UpdateCardsInfo message)
        {
            if (message.LordCard == 0)
            {
                Unit myUnit = UnitHelper.GetMyUnitFromZoneScene(session.ZoneScene());
                await CreateAndDisplayCard(message.CardsInfo, myUnit);
            }
            else
            {
                await CreateAndDisplayCard(message.CardsInfo, session.ZoneScene().CurrentScene());
            }

        }

        private async ETTask CreateAndDisplayCard(List<CardInfo> cardsInfo, Unit myUnit)
        {
            ListComponent<Card> tempCards = ListComponent<Card>.Create();
            var handCards = myUnit.GetComponent<HandCardsComponent>();
            bool isFirstTime = handCards.CardsCount == 0;

            foreach (var info in cardsInfo)
            {
                var card = CardFactory.CreateCard(myUnit, info.CardWeight, info.CardSuit);
                handCards.AddCard(card);
                tempCards.Add(card);
            }

            if (!isFirstTime)
            {
                tempCards.Clear();
                tempCards.AddRange(handCards.Library);
            }
            
            handCards.Sort();
            tempCards.Sort();

            for (var i = tempCards.Count - 1; i >= 0; i--)
            {
                Game.EventSystem.Publish(new AfterCardCreate() { ZoneScene = myUnit.ZoneScene(), Card = tempCards[i] });
                if (isFirstTime)
                    await TimerComponent.Instance.WaitAsync(200);
            }

            Game.EventSystem.Publish(new AfterCardCreateEnd() { ZoneScene = myUnit.ZoneScene() });

            tempCards.Dispose();
        }

        private async ETTask CreateAndDisplayCard(List<CardInfo> cardsInfo, Scene currentScene)
        {
            ListComponent<Card> tempCards = ListComponent<Card>.Create();
            LandRoomComponent landRoom = currentScene.GetComponent<LandRoomComponent>();
            foreach (var info in cardsInfo)
            {
                var card = CardFactory.CreateCard(landRoom, info.CardWeight, info.CardSuit);
                landRoom.GetComponent<HandCardsComponent>().AddCard(card);
                tempCards.Add(card);
            }

            for (var i = tempCards.Count - 1; i >= 0; i--)
            {
                Game.EventSystem.Publish(new AfterLordCardCreate() { ZoneScene = currentScene.ZoneScene(), Card = tempCards[i] });
            }

            tempCards.Dispose();

            await ETTask.CompletedTask;
        }
    }
}