using ET.EventType;

namespace ET
{
	public class AfterLordCardCreate_CreateView : AEvent<AfterLordCardCreate>
	{
		protected override void Run(AfterLordCardCreate a)
		{
			a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().AddCardSprite(a.Card, true);
		}
	}
}