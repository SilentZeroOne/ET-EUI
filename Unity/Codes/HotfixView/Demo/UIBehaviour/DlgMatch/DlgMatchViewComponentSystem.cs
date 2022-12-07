
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMatchViewComponentAwakeSystem : AwakeSystem<DlgMatchViewComponent> 
	{
		public override void Awake(DlgMatchViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMatchViewComponentDestroySystem : DestroySystem<DlgMatchViewComponent> 
	{
		public override void Destroy(DlgMatchViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
