using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgLandCreateRole))]
	public static  class DlgLandCreateRoleSystem
	{

		public static void RegisterUIEvent(this DlgLandCreateRole self)
		{
			self.View.E_ConfirmButton.AddListenerAsync(self.OnConfirmBtnOnClickHandler);
		}

		public static void ShowWindow(this DlgLandCreateRole self, Entity contextData = null)
		{
		}

		public static async ETTask OnConfirmBtnOnClickHandler(this DlgLandCreateRole self)
		{
			if (self.IsCreating)
			{
				return;
			}

			self.IsCreating = true;

			try
			{
				var nickName = self.View.E_NickNameInputField.text;
				if (string.IsNullOrEmpty(nickName))
				{
					self.View.E_PromptText.SetText("请输入昵称!");
					self.IsCreating = false;
					return;
				}

				int errorCode = await LoginHelper.CreateRole(self.ZoneScene(), nickName);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					self.IsCreating = false;
					return;
				}

				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_LandCreateRole);
				self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_LandLobby);
			}
			catch (Exception e)
			{
				self.IsCreating = false;
				Log.Error(e.ToString());
			}
		}
		 

	}
}
