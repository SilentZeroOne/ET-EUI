
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgInventoryViewComponentAwakeSystem : AwakeSystem<DlgInventoryViewComponent> 
	{
		public override void Awake(DlgInventoryViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgInventoryViewComponentDestroySystem : DestroySystem<DlgInventoryViewComponent> 
	{
		public override void Destroy(DlgInventoryViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
