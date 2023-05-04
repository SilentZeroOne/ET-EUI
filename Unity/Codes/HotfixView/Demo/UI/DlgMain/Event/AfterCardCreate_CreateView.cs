using ET.EventType;

namespace ET
{
	public class AfterCardCreate_CreateView : AEvent<AfterCardCreate>
	{
		protected override void Run(AfterCardCreate a)
		{
			a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().HideBeforeStartUI();
			a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().AddCardSprite(a.Card, false);
		}
	}
}