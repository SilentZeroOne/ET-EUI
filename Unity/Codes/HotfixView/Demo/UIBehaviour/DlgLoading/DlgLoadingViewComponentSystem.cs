
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgLoadingViewComponentAwakeSystem : AwakeSystem<DlgLoadingViewComponent> 
	{
		public override void Awake(DlgLoadingViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
			self.UpdateLoadingText().Coroutine();
		}
	}


	[ObjectSystem]
	public class DlgLoadingViewComponentDestroySystem : DestroySystem<DlgLoadingViewComponent> 
	{
		public override void Destroy(DlgLoadingViewComponent self)
		{
			self.DestroyWidget();
		}
	}

	public static class DlgLoadingViewComponentSystem
	{
		public static async ETTask UpdateLoadingText(this DlgLoadingViewComponent self)
		{
			int dotCount = 0;
			self.ELabel_LoadingText.SetText("Loading");
			while (self != null)
			{
				if (dotCount == 3)
				{
					dotCount = 0;
					self.ELabel_LoadingText.SetText("Loading");
				}
				else
				{
					self.ELabel_LoadingText.text += ".";
					dotCount++;
				}
				await TimerComponent.Instance.WaitAsync(500);
			}
		}
	}
}
