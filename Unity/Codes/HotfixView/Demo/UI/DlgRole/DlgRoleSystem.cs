using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgRoleSystem
	{

		public static void RegisterUIEvent(this DlgRole self)
		{
			self.View.EButton_CreateRoleButton.AddListenerAsync(() => { return self.OnCreateRoleClickHandler();});
			self.View.ELoopScrollList_RolesListLoopHorizontalScrollRect.AddItemRefreshListener(((transform, i) =>
					self.OnScrollItemRefreshHandler(transform, i)));
			
			
		}

		public static void ShowWindow(this DlgRole self, Entity contextData = null)
		{
			int count = self.ZoneScene().GetComponent<RoleInfosComponent>().RoleInfos.Count;
			self.AddUIScrollItems(ref self.ScrollItemRoles,count);
			self.View.ELoopScrollList_RolesListLoopHorizontalScrollRect.SetVisible(true, count);
		}

		public static void HideWindow(this DlgRole self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemRoles);
		}

		public static void OnScrollItemRefreshHandler(this DlgRole self, Transform transform, int index)
		{
			Scroll_Item_Role itemRole = self.ScrollItemRoles[index].BindTrans(transform);
			RoleInfo roleInfo = self.ZoneScene().GetComponent<RoleInfosComponent>().RoleInfos[index];
			itemRole.E_NameText.SetText(roleInfo.Name);
			itemRole.E_CreateTimeText.SetText(roleInfo.CreateTime.ToString());
		}

		public static async ETTask OnCreateRoleClickHandler(this DlgRole self)
		{
			try
			{
				var errorCode = await LoginHelper.CreateRole(self.ZoneScene(), self.View.EInputField_NameInputInputField.text);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				
				self.View.ELoopScrollList_RolesListLoopHorizontalScrollRect.RefreshCells();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
			
			
			await ETTask.CompletedTask;
		}
		 

	}
}
