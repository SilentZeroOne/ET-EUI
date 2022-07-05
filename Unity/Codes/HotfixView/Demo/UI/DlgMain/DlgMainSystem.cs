using System.Collections;
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
			self.View.EButton_AdvantureButton.AddListenerAsync(() => { return self.OnAdventureButtonClickHandler(); });
			self.View.EButton_BagButton.AddListenerAsync(() => { return self.OnBagButtonClickHandler(); });
			RedDotHelper.AddRedDotNodeView(self.ZoneScene(), "Role", self.View.EButton_RoleButton.gameObject, Vector3.one, new Vector2(95, 35));
		}

		public static void ShowWindow(this DlgMain self, Entity contextData = null)
		{
			self.Refresh().Coroutine();	
		}

		public static void OnUnLoadWindow(this DlgMain self)
		{
			RedDotMonoView monoView = self.View.EButton_RoleButton.GetComponent<RedDotMonoView>();
			RedDotHelper.RemoveRedDotView(self.ZoneScene(), "Role", out monoView);
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

		public static async ETTask OnAdventureButtonClickHandler(this DlgMain self)
		{
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Adventure);

			await ETTask.CompletedTask;
		}
		
		public static async ETTask OnBagButtonClickHandler(this DlgMain self)
		{
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Bag);

			await ETTask.CompletedTask;
		}
	}
}
