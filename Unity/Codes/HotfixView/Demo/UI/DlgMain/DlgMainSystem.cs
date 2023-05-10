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

			self.View.E_RobButton.AddListenerAsyncWithId(self.OnRobBtnClickHandler, 1);
			self.View.E_NotRobButton.AddListenerAsyncWithId(self.OnRobBtnClickHandler, 0);
			
			self.ReadyIcon.Add(self.View.EG_SelfStandByRectTransform);
			self.ReadyIcon.Add(self.View.EG_Player1StandByRectTransform);
			self.ReadyIcon.Add(self.View.EG_Player2StandByRectTransform);
			
			self.Promt.Add(self.View.E_SelfPromtTextMeshProUGUI);
			self.Promt.Add(self.View.E_Player1PromtTextMeshProUGUI);
			self.Promt.Add(self.View.E_Player2PromtTextMeshProUGUI);
			
			GameObjectPoolHelper.InitPool("PlayCard", 17);
		}

		public static void ShowWindow(this DlgMain self, Entity contextData = null)
		{
			self.View.EG_AfterStartGameButtonsRectTransform.SetVisible(false);
			for (int i = 0; i < 3; i++)
			{
				self.SetReadyIcon(i, false);
			}
		}

		public static async ETTask OnReadyBtnClickHandler(this DlgMain self, int ready)
		{
			Scene zoneScene = self.ZoneScene();
			Unit myUnit = UnitHelper.GetMyUnitFromZoneScene(zoneScene);

			int errorCode = await MatchHelper.SetReadyNetwork(zoneScene, myUnit.Id, ready);
			if (errorCode != ErrorCode.ERR_Success)
			{
				Log.Error(errorCode.ToString());
				return;
			}
			
			self.View.E_StartGameButton.SetVisible(false);
			self.View.E_UnReadyButton.SetVisible(true);
		}

		public static async ETTask OnRobBtnClickHandler(this DlgMain self, int grab)
		{
			Scene zoneScene = self.ZoneScene();
			Unit myUnit = UnitHelper.GetMyUnitFromZoneScene(zoneScene);

			int errorCode = await MatchHelper.RobLandLord(zoneScene, myUnit.Id, grab);
			if (errorCode != ErrorCode.ERR_Success)
			{
				Log.Error(errorCode.ToString());
				return;
			}

			self.SetRobBtnVisible(false);
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
		}

		public static void SetPromt(this DlgMain self, int unitIndex, bool active,bool rob)
		{
			self.Promt[unitIndex].SetVisible(active);
			self.Promt[unitIndex].SetText(rob? "抢地主" : "不抢");
		}

		public static void SetMultiples(this DlgMain self, int multiples)
		{
			self.View.E_MultiplesTextMeshProUGUI.SetText(multiples.ToString());
		}

		public static void HideBeforeStartUI(this DlgMain self)
		{
			foreach (var icon in self.ReadyIcon)
			{
				icon.SetVisible(false);
			}

			foreach (var promt in self.Promt)
			{
				promt.SetVisible(false);
			}
			
			self.View.E_StartGameButton.SetVisible(false);
			self.View.E_UnReadyButton.SetVisible(false);
		}

		public static void AddCardSprite(this DlgMain self, Card card, bool lordCard)
		{
			var newCard = GameObjectPoolHelper.GetObjectFromPool("PlayCard");
			self.Cards.Add(card.Id, newCard);

			if (lordCard)
				self.View.EG_LordCardBgRectTransform.SetVisible(true);
			newCard.transform.SetParent(lordCard? self.View.EG_LordCardBgRectTransform : self.View.EG_CardParentRectTransform);
			var rect = newCard.transform.GetComponent<RectTransform>();
			rect.localPosition = Vector3.zero;
			rect.localScale = Vector2.one * (lordCard? 1 : 1.5f);
			newCard.GetComponent<Image>().sprite = CardHelper.GetCardSprite(card);

			card.AddComponent<GameObjectComponent>().SetGameObject(newCard);
		}

		public static void ClearAllCard(this DlgMain self)
		{
			foreach (var card in self.Cards)
			{
				GameObjectPoolHelper.ReturnObjectToPool(card.Value);
			}
			
			self.Cards.Clear();
		}

		public static void DisplayGamingButtons(this DlgMain self, bool playCard)
		{
			self.View.EG_AfterStartGameButtonsRectTransform.SetVisible(true);
			self.SetRobBtnVisible(!playCard);
			self.View.E_PassButton.SetVisible(playCard);
			self.View.E_PlayCardButton.SetVisible(playCard);
		}

		private static void SetRobBtnVisible(this DlgMain self, bool visiable)
		{
			self.View.E_RobButton.SetVisible(visiable);
			self.View.E_NotRobButton.SetVisible(visiable);
		}
	}
}
