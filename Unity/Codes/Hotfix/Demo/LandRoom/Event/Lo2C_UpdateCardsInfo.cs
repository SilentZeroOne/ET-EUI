using System;
using System.Collections.Generic;
using ET.EventType;

namespace ET
{
    [FriendClassAttribute(typeof(ET.Card))]
    public class Lo2C_UpdateCardsInfoHandler : AMHandler<Lo2C_UpdateCardsInfo>
    {
        protected override async void Run(Session session, Lo2C_UpdateCardsInfo message)
        {
            Unit myUnit = UnitHelper.GetMyUnitFromZoneScene(session.ZoneScene());
            await CreateAndDisplayCard(message.CardsInfo, myUnit);
        }

        private async ETTask CreateAndDisplayCard(List<CardInfo> cardsInfo, Unit myUnit)
        {
            ListComponent<Card> tempCards = ListComponent<Card>.Create();
            foreach (var info in cardsInfo)
            {
                var card = CardFactory.CreateCard(myUnit, info.CardWeight, info.CardSuit);
                myUnit.GetComponent<HandCardsComponent>().AddCard(card);
                tempCards.Add(card);
            }

            tempCards.Sort();

            for (var i = tempCards.Count - 1; i >= 0; i--)
            {
                Game.EventSystem.Publish(new AfterCardCreate() { ZoneScene = myUnit.ZoneScene(), Card = tempCards[i] });
                await TimerComponent.Instance.WaitAsync(200);
            }
            
            tempCards.Dispose();
        }
    }
}