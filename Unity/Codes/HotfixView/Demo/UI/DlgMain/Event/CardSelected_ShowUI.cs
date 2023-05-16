using ET.EventType;

namespace ET
{
	public class CardSelected_ShowUI: AEvent<CardSelected>
	{
		protected override void Run(CardSelected a)
		{
			Log.Debug($"{a.CardId} {a.IsSelected}");
			a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().SelectCard(a.CardId, a.IsSelected);
		}
	}
}