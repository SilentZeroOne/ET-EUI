using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgMain))]
	public static  class DlgMainSystem
	{

		public static void RegisterUIEvent(this DlgMain self)
		{
			self.View.E_InventoryButton.AddListener(self.OnInventoryButtonClick);
		}

		public static void ShowWindow(this DlgMain self, Entity contextData = null)
		{
		}

		public static void OnInventoryButtonClick(this DlgMain self)
		{
			var uiComponent = self.ZoneScene().GetComponent<UIComponent>();
			bool visiable = uiComponent.IsWindowVisible(WindowID.WindowID_Inventory);
			if (visiable)
			{
				uiComponent.HideWindow(WindowID.WindowID_Inventory);
			}
			else
			{
				uiComponent.ShowWindow(WindowID.WindowID_Inventory);
			}
		}
		 

	}
}
