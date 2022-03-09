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
			self.View.EButton_DeleteRoleButton.AddListenerAsync(() => { return self.OnDeleteRoleClickHandler();});
			
		}

		public static void ShowWindow(this DlgRole self, Entity contextData = null)
		{
			self.RefreshRoleItem();
		}

		public static void RefreshRoleItem(this DlgRole self)
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
			itemRole.EButton_SelectRoleImage.color =
					self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId == roleInfo.Id? Color.red : Color.cyan; 
			itemRole.E_NameText.SetText(roleInfo.Name);
			itemRole.E_CreateTimeText.SetText(roleInfo.CreateTime.ToString());
			itemRole.EButton_SelectRoleButton.AddListener(()=>{self.OnSelectRoleItemHandler(roleInfo.Id);});
		}

		public static void OnSelectRoleItemHandler(this DlgRole self, long roleId)
		{
			self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId = roleId;
			Log.Debug($"Current RoleID ={roleId}");
			self.View.ELoopScrollList_RolesListLoopHorizontalScrollRect.RefillCells();
		}

		public static async ETTask OnCreateRoleClickHandler(this DlgRole self)
		{
			var name = self.View.EInputField_NameInputInputField.text;
			if (string.IsNullOrEmpty(name))
			{
				Log.Error($"请填写角色名");
				return;
			}
			
			try
			{
				var errorCode = await LoginHelper.CreateRole(self.ZoneScene(), name);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				
				self.RefreshRoleItem();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

		public static async ETTask OnDeleteRoleClickHandler(this DlgRole self)
		{
			var currentId = self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId;
			if (currentId == 0)
			{
				Log.Error("Please select a role to delete");
				return;
			}

			try
			{
				var errorCode = await LoginHelper.DeleteRole(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				
				self.RefreshRoleItem();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
	}
}
