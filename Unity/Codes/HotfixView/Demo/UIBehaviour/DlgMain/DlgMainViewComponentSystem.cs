
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMainViewComponentAwakeSystem : AwakeSystem<DlgMainViewComponent> 
	{
		public override void Awake(DlgMainViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
			self.uiRectTransform = self.uiTransform.GetComponent<RectTransform>();
		}
	}


	[ObjectSystem]
	public class DlgMainViewComponentDestroySystem : DestroySystem<DlgMainViewComponent> 
	{
		public override void Destroy(DlgMainViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
