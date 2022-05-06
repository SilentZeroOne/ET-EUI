﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgMainSystem
	{

		public static void RegisterUIEvent(this DlgMain self)
		{
			self.View.EButton_RoleButton.AddListenerAsync(() => { return self.OnRoleButtonClickHandler(); });
		}

		public static void ShowWindow(this DlgMain self, Entity contextData = null)
		{
			self.Refresh().Coroutine();	
		}

		public static async ETTask Refresh(this DlgMain self)
		{
			Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
			NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

			self.View.E_LevelText.SetText(numericComponent.GetAsInt(NumericType.Level).ToString());
			self.View.E_GoldText.SetText(numericComponent.GetAsInt(NumericType.Gold).ToString());
			self.View.E_ExperienceText.SetText(numericComponent.GetAsInt(NumericType.Exp).ToString());

			await ETTask.CompletedTask;
		}

		public static async ETTask OnRoleButtonClickHandler(this DlgMain self)
		{
			// try
			// {
			// 	int error = await NumericHelper.TestUpdateNumeric(self.ZoneScene());
			// 	if (error != ErrorCode.ERR_Success)
			// 	{
			// 		Log.Error(error.ToString());
			// 		return;
			// 	}
			// 	
			// 	Log.Debug("发送测试数值组件成功");
			// 	
			// }
			// catch (Exception e)
			// {
			// 	Log.Error(e.ToString());
			// }
			
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_RoleInfo);

			await ETTask.CompletedTask;
		}

	}
}
