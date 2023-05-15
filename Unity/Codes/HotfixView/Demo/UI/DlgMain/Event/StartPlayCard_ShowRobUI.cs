using ET.EventType;

namespace ET
{
	public class StartPlayCard_ShowRobUI : AEvent<StartPlayCard>
	{
		protected override void Run(StartPlayCard a)
		{
			a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().DisplayGamingButtons(a.PlayingCard, a.IsSelf);
		}
	}
}