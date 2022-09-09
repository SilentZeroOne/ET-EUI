
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgCursorViewComponentAwakeSystem : AwakeSystem<DlgCursorViewComponent> 
	{
		public override void Awake(DlgCursorViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
			self.uiRectTransform = self.uiTransform.GetComponent<RectTransform>();
		}
	}


	[ObjectSystem]
	public class DlgCursorViewComponentDestroySystem : DestroySystem<DlgCursorViewComponent> 
	{
		public override void Destroy(DlgCursorViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
