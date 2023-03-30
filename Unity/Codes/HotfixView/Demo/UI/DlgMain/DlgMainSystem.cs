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
			self.View.E_StartGameButton.AddListenerAsyncWithId(self.OnReadyBtnClickHandler, 1);
			self.View.E_UnReadyButton.AddListenerAsyncWithId(self.OnReadyBtnClickHandler, 0);
			
			self.ReadyIcon.Add(self.View.EG_SelfStandByRectTransform);
			self.ReadyIcon.Add(self.View.EG_Player1StandByRectTransform);
			self.ReadyIcon.Add(self.View.EG_Player2StandByRectTransform);
		}

		public static void ShowWindow(this DlgMain self, Entity contextData = null)
		{
			for (int i = 0; i < 3; i++)
			{
				self.SetReadyIcon(i, false);
			}
		}

		public static async ETTask OnReadyBtnClickHandler(this DlgMain self, int ready)
		{
			Scene currentScene = self.ZoneScene().CurrentScene();
			Unit myUnit = UnitHelper.GetMyUnitFromCurrentScene(currentScene);
			LandRoomComponent landRoomComponent = currentScene.GetComponent<LandRoomComponent>();

			int errorCode = await landRoomComponent?.SetReadyNetwork(myUnit.Id, ready);
			if (errorCode != ErrorCode.ERR_Success)
			{
				Log.Error(errorCode.ToString());
				return;
			}
		}

		public static void SetReadyIcon(this DlgMain self,int unitIndex ,bool active)
		{
			self.ReadyIcon[unitIndex].SetVisible(active);
			if (unitIndex == 0)
			{
				self.View.E_StartGameButton.SetVisible(!active);
				self.View.E_UnReadyButton.SetVisible(active);
			}
		}
	}
}
