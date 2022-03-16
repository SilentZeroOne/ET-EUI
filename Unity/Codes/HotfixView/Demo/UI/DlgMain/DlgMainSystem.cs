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
		 
		}

		public static void ShowWindow(this DlgMain self, Entity contextData = null)
		{
			self.Refresh().Coroutine();	
		}

		public static async ETTask Refresh(this DlgMain self)
		{
			Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
			NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

			self.View.E_LevelText.SetText(numericComponent.GetAsInt((int)NumericType.Level).ToString());
			self.View.E_GoldText.SetText(numericComponent.GetAsInt((int)NumericType.Gold).ToString());
			self.View.E_ExperienceText.SetText(numericComponent.GetAsInt((int)NumericType.Exp).ToString());

			await ETTask.CompletedTask;
		}
		 

	}
}
