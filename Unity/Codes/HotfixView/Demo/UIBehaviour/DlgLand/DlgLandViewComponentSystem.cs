
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgLandViewComponentAwakeSystem : AwakeSystem<DlgLandViewComponent> 
	{
		public override void Awake(DlgLandViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgLandViewComponentDestroySystem : DestroySystem<DlgLandViewComponent> 
	{
		public override void Destroy(DlgLandViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
