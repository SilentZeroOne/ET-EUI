using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[Timer(TimerType.RankUI)]
	public class RankUITimer: ATimer<DlgRank>
	{
		public override void Run(DlgRank self)
		{
			self?.RefreshRankInfo().Coroutine();
		}
	}
	
	[FriendClass(typeof(DlgRank))]
	[FriendClass(typeof(Scroll_Item_rank))]
	[FriendClass(typeof(RankInfo))]
	public static  class DlgRankSystem
	{

		public static void RegisterUIEvent(this DlgRank self)
		{
			self.RegisterCloseEvent<DlgRank>(self.View.E_CloseButton);
			self.View.E_RanksLoopVerticalScrollRect.AddItemRefreshListener(self.OnRankItemRefreshHandler);
		}

		public static void ShowWindow(this DlgRank self, Entity contextData = null)
		{
			self.RefreshRankInfo().Coroutine();
			self.Timer = TimerComponent.Instance.NewRepeatedTimer(5000, TimerType.RankUI, self);
		}

		public static void HideWindow(this DlgRank self)
		{
			//self.RemoveUIScrollItems(ref self.ScrollItemRanks);
			TimerComponent.Instance.Remove(ref self.Timer);
		}

		public static async ETTask RefreshRankInfo(this DlgRank self)
		{
			try
			{
				Scene zoneScene = self.ZoneScene();
				int errCode = await RankHelper.GetRankInfo(zoneScene);
				if (errCode != ErrorCode.ERR_Success)
				{
					Log.Error(errCode.ToString());
					return;
				}

				if (!zoneScene.GetComponent<UIComponent>().IsWindowVisible(WindowID.WindowID_Rank))
				{
					return;
				}

				int count = zoneScene.GetComponent<RankComponent>().GetRankCount();
				self.AddUIScrollItems(ref self.ScrollItemRanks, count);
				self.View.E_RanksLoopVerticalScrollRect.SetVisible(true, count);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
			
			await ETTask.CompletedTask;
		}

		public static void OnRankItemRefreshHandler(this DlgRank self, Transform transform, int index)
		{
			Scroll_Item_rank scrollItemRank = self.ScrollItemRanks[index].BindTrans(transform);
			RankInfo rankInfo = self.ZoneScene().GetComponent<RankComponent>().GetRankInfoByIndex(index);

			scrollItemRank.E_RankOrderText.text = $"第{index+1}名";
			scrollItemRank.E_NameText.text = rankInfo.Name;
			scrollItemRank.E_LevelText.SetText($"Lv.{rankInfo.Count}");
		}
	}
}
