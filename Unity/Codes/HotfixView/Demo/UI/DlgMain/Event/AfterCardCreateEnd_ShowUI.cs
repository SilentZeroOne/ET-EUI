using ET.EventType;

namespace ET
{
	public class AfterCardCreateEnd_ShowUI: AEvent<AfterCardCreateEnd>
	{
		protected override void Run(AfterCardCreateEnd a)
		{
			//a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().DisplayRobLandLordButtons();
		}
	}
}