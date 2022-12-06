
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgLandCreateRoleViewComponentAwakeSystem : AwakeSystem<DlgLandCreateRoleViewComponent> 
	{
		public override void Awake(DlgLandCreateRoleViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgLandCreateRoleViewComponentDestroySystem : DestroySystem<DlgLandCreateRoleViewComponent> 
	{
		public override void Destroy(DlgLandCreateRoleViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
