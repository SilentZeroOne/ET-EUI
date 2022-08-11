﻿
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgDragItemViewComponentAwakeSystem : AwakeSystem<DlgDragItemViewComponent> 
	{
		public override void Awake(DlgDragItemViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgDragItemViewComponentDestroySystem : DestroySystem<DlgDragItemViewComponent> 
	{
		public override void Destroy(DlgDragItemViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
