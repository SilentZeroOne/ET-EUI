using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgLoginSystem
	{

		public static void RegisterUIEvent(this DlgLogin self)
		{
			self.View.E_LoginButton.AddListenerAsync(self.OnLoginClickHandler);
		}

		public static void ShowWindow(this DlgLogin self, Entity contextData = null)
		{
			self.RevertLastAccount();
		}
		
		public static async ETTask OnLoginClickHandler(this DlgLogin self)
		{
			try
			{
				self.SaveLastAccount();
				int errorCode = await LoginHelper.Login(
					self.DomainScene(), 
					ConstValue.LoginAddress, 
					self.View.E_AccountInputField.text, 
					self.View.E_PasswordInputField.text);

				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}

				errorCode = await LoginHelper.GetServerInfo(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Login);
				self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Server);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
				throw;
			}
		}

		public static void SaveLastAccount(this DlgLogin self)
		{
			PlayerPrefs.SetString("account", self.View.E_AccountInputField.text);
			PlayerPrefs.SetString("password", self.View.E_PasswordInputField.text);
		}

		public static void RevertLastAccount(this DlgLogin self)
		{
			self.View.E_AccountInputField.SetTextWithoutNotify(PlayerPrefs.GetString("account", ""));
			self.View.E_PasswordInputField.SetTextWithoutNotify(PlayerPrefs.GetString("password", ""));
		}
		
		public static void HideWindow(this DlgLogin self)
		{

		}
	}
}
