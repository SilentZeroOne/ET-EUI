using System.Collections;
using System.Collections.Generic;
using System;
using BM;
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
			self.View.E_LeaveRoomButton.AddListener(self.OnLeaveRoomBtnClickHandler);
			
			self.ReadyIcon.Add(self.View.EG_SelfStandByRectTransform);
			self.ReadyIcon.Add(self.View.EG_Player1StandByRectTransform);
			self.ReadyIcon.Add(self.View.EG_Player2StandByRectTransform);
			
			GameObjectPoolHelper.InitPool("PlayCard", 17);
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

		public static void OnLeaveRoomBtnClickHandler(this DlgMain self)
		{
			Scene currentScene = self.ZoneScene().CurrentScene();
			Unit myUnit = UnitHelper.GetMyUnitFromCurrentScene(currentScene);
			LandRoomComponent landRoomComponent = currentScene.GetComponent<LandRoomComponent>();

			landRoomComponent.LeaveRoom(myUnit.Id);
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

		public static void HideBeforeStartUI(this DlgMain self)
		{
			foreach (var icon in self.ReadyIcon)
			{
				icon.SetVisible(false);
			}
			
			self.View.E_StartGameButton.SetVisible(false);
			self.View.E_UnReadyButton.SetVisible(false);
		}

		public static void AddCardSprite(this DlgMain self, Card card, bool lordCard)
		{
			var newCard = GameObjectPoolHelper.GetObjectFromPool("PlayCard");
			self.Cards.Add(card.Id, newCard);

			newCard.transform.SetParent(lordCard? self.View.EG_LordCardBgRectTransform : self.View.EG_CardParentRectTransform);
			var rect = newCard.transform.GetComponent<RectTransform>();
			rect.localPosition = Vector3.zero;
			rect.localScale = Vector2.one * (lordCard? 1 : 1.5f);
			newCard.GetComponent<Image>().sprite = CardHelper.GetCardSprite(card);

			card.AddComponent<GameObjectComponent>().SetGameObject(newCard);
		}
	}
}
