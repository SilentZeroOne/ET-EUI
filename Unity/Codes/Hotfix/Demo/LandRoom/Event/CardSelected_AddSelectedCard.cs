using ET.EventType;

namespace ET
{
	public class CardSelected_AddSelectedCard: AEvent<CardSelected>
	{
		protected override void Run(CardSelected a)
		{
			a.ZoneScene.CurrentScene().GetComponent<LandInteractionComponent>().AddOrRemoveSelectedCard(a.CardId, a.IsSelected);
		}
	}
}