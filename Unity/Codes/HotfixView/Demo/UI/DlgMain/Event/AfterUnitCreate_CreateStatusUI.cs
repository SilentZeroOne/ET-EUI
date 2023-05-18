using ET.EventType;

namespace ET
{
	public class AfterUnitCreate_CreateStatusUI : AEvent<AfterUnitCreate>
	{
		protected override async void Run(AfterUnitCreate a)
		{
			if (a.CreateView)
			{
				var dlgMain = a.Unit.ZoneScene().GetComponent<UIComponent>()?.GetDlgLogic<DlgMain>();
				if (dlgMain == null)
				{
					await a.Unit.ZoneScene().GetComponent<ObjectWait>().Wait<WaitType.Wait_MainUILoad>();
					a.Unit.ZoneScene().GetComponent<UIComponent>()?.GetDlgLogic<DlgMain>().DisplayPlayerStatus(a.Unit.Id);
				}
				else
				{
					dlgMain.DisplayPlayerStatus(a.Unit.Id);
				}
			}
		}
	}
}