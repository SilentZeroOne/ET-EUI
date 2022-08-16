
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgItemTooltipViewComponentAwakeSystem : AwakeSystem<DlgItemTooltipViewComponent> 
	{
		public override void Awake(DlgItemTooltipViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
			self.uiRectTransform = self.uiTransform.GetComponent<RectTransform>();
		}
	}


	[ObjectSystem]
	public class DlgItemTooltipViewComponentDestroySystem : DestroySystem<DlgItemTooltipViewComponent> 
	{
		public override void Destroy(DlgItemTooltipViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
