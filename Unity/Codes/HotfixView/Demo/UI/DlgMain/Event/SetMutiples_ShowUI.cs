using ET.EventType;

namespace ET
{
	public class SetMutiples_ShowUI : AEvent<SetMutiples>
	{
		protected override void Run(SetMutiples a)
		{
			a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().SetMultiples(a.Mutiples);
		}
	}
}