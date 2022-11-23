using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgLand))]
	public static  class DlgLandSystem
	{

		public static void RegisterUIEvent(this DlgLand self)
		{
			self.View.E_LoginButton.AddListenerAsync(self.OnLoginBtnClick);
			self.View.E_RegisterButton.AddListenerAsync(self.OnRegisterBtnClick);
		}

		public static void ShowWindow(this DlgLand self, Entity contextData = null)
		{
		}

		public static async ETTask OnLoginBtnClick(this DlgLand self)
		{
			if (self.IsLogining || self.IsDisposed)
			{
				return;
			}

			self.IsLogining = true;

			try
			{
				int errCode = await LoginHelper.Login(self.ZoneScene(), ConstValue.LoginAddress, self.View.E_AccountInputField.text,
					self.View.E_PasswordInputField.text);
				
				if (errCode != ErrorCode.ERR_Success)
				{
					Log.Error(errCode.ToString());
					return;
				}

				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Land);
				//TODO 展示下一个界面或者场景
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
				throw;
			}
			
			await ETTask.CompletedTask;
		}
		
		public static async ETTask OnRegisterBtnClick(this DlgLand self)
		{
			if (self.IsRegistering || self.IsDisposed)
			{
				return;
			}

			self.IsRegistering = true;
			
			await ETTask.CompletedTask;
		}


	}
}
