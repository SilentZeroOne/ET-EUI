using ET.EventType;

namespace ET
{
	public class RobLandLord_ShowUI : AEvent<RobLandLord>
	{
		protected override void Run(RobLandLord a)
		{
			a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().SetPromt(a.UnitIndex, true, a.Rob);
		}
	}
}