using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ET
{
	[FriendClass(typeof(DlgAdventure))]
	public static  class DlgAdventureSystem
	{

		public static void RegisterUIEvent(this DlgAdventure self)
		{
			self.RegisterCloseEvent<DlgAdventure>(self.View.E_CloseButton);
			self.View.E_LevelDetailLoopVerticalScrollRect.AddItemRefreshListener(((transform, i) =>
			{
				self.OnLevelDetailItemRefreshHandler(transform, i);
			}));
		}

		public static void ShowWindow(this DlgAdventure self, Entity contextData = null)
		{
			self.View.EG_ContentRectTransform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.0f);
			self.View.EG_ContentRectTransform.DOScale(Vector3.one, 0.3f).onComplete += () => { self.Refresh(); };
		}

		public static void HideWindow(this DlgAdventure self)
		{
			self.View.E_LevelDetailLoopVerticalScrollRect.SetVisible(false);
			self.RemoveUIScrollItems(ref self.ScrollItemLevelDetails);
		}
		
		public static void Refresh(this DlgAdventure self)
		{
			int count = BattleLevelConfigCategory.Instance.GetAll().Count;
			self.AddUIScrollItems(ref self.ScrollItemLevelDetails, count);
			self.View.E_LevelDetailLoopVerticalScrollRect.SetVisible(true, count);
		}

		public static void OnLevelDetailItemRefreshHandler(this DlgAdventure self, Transform transform, int index)
		{
			Scroll_Item_LevelDetail item = self.ScrollItemLevelDetails[index].BindTrans(transform);
			var config = BattleLevelConfigCategory.Instance.GetConfigByIndex(index);
			
			NumericComponent numericComponent = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene());
			int unitLevel = numericComponent.GetAsInt(NumericType.Level);
			bool isInAdventure = numericComponent.GetAsInt(NumericType.AdventureState) != 0;

			if (unitLevel >= config.MinEnterLevel[0])
			{
				if (!isInAdventure)
				{
					item.E_CurrentStatusText.SetVisible(false);
					item.E_EnterButton.SetVisible(true);
				}
				else
				{
					item.E_CurrentStatusText.SetVisible(true);
					item.E_EnterButton.SetVisible(false);
					item.E_CurrentStatusText.SetText("战斗中...");
				}
			}
			else
			{
				item.E_CurrentStatusText.SetVisible(true);
				item.E_EnterButton.SetVisible(false);
				item.E_CurrentStatusText.SetText("未达到进入等级");
			}
			
			item.E_LevelTitleText.SetText(config.Name);
			item.E_LevelEnterText.SetText($"Lv.{config.MinEnterLevel[0]}~Lv.{config.MinEnterLevel[1]}");
			item.E_EnterButton.AddListenerAsync(() => { return self.OnStartGameLevelClickHandler(config.Id); });
		}

		public static async ETTask OnStartGameLevelClickHandler(this DlgAdventure self, int levelId)
		{
			try
			{
				int errorCode = await AdventureHelper.RequestStartGameLevel(self.ZoneScene(), levelId);
				if (errorCode != ErrorCode.ERR_Success)
				{
					return;
				}
				
				//self.Refresh();
				//Game.EventSystem.Publish(new EventType.StartGameLevel(){ZoneScene = self.ZoneScene()});
				
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Adventure);

				self.ZoneScene().CurrentScene().GetComponent<AdventureComponent>().StartAdventure().Coroutine();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
		
		 

	}
}
