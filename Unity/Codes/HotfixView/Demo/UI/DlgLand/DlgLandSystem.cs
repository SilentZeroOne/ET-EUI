using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
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
			self.View.E_AccountInputField.text = PlayerPrefs.GetString("AccountName");
			self.View.E_PasswordInputField.text = PlayerPrefs.GetString("AccountPassword");
		}

		public static async ETTask OnLoginBtnClick(this DlgLand self)
		{
			if (self.IsLogining || self.IsDisposed || self.IsRegistering)
			{
				return;
			}

			self.IsLogining = true;

			try
			{
				var accountName = self.View.E_AccountInputField.text.Trim();
				var accountPassword = self.View.E_PasswordInputField.text;

				int errCode = await LoginHelper.Login(self.ZoneScene(), ConstValue.LoginAddress, accountName, accountPassword);
				
				if (errCode != ErrorCode.ERR_Success)
				{
					self.IsLogining = false;
					return;
				}

				PlayerPrefs.SetString("AccountName", accountName);
				PlayerPrefs.SetString("AccountPassword", accountPassword);

				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Land);
				
				var infoComponent = self.ZoneScene().GetComponent<RoleInfoComponent>();
				self.DomainScene().GetComponent<UIComponent>()
						.ShowWindow(infoComponent.RoleInfo == null? WindowID.WindowID_LandCreateRole : WindowID.WindowID_LandLobby);

				self.IsLogining = false;
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
				self.IsLogining = false;
				throw;
			}
			
			await ETTask.CompletedTask;
		}
		
		public static async ETTask OnRegisterBtnClick(this DlgLand self)
		{
			if (self.IsRegistering || self.IsDisposed || self.IsLogining)
			{
				return;
			}

			self.IsRegistering = true;
			
			var accountName = self.View.E_AccountInputField.text.Trim();
			var accountPassword = self.View.E_PasswordInputField.text;
			
			if (string.IsNullOrEmpty(accountName) || string.IsNullOrEmpty(accountPassword))
			{
				Log.Error("Account name or password can't be empty");
				return;
			}

			if (!Regex.IsMatch(accountName, @"^(?=.*[a-z].*)(?=.*[A-Z].*).{4,15}$"))
			{
				Log.Error("Account name must contains a-zA-z and more than 3 characters");
				return;
			}

			if (!Regex.IsMatch(accountPassword, @"^[A-Za-z0-9]+$"))
			{
				Log.Error("Password must contains a-zA-z0-9");
				return;
			}
			
			try
			{
				int errCode = await LoginHelper.Register(self.ZoneScene(), ConstValue.LoginAddress, accountName, accountPassword);
				
				if (errCode != ErrorCode.ERR_Success)
				{
					self.IsRegistering = false;
					return;
				}
				
				self.IsRegistering = false;
				
				//注册结束直接进入登陆流程
				self.OnLoginBtnClick().Coroutine();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
				self.IsRegistering = false;
				throw;
			}
		}
	}
}
