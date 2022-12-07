using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgMatch))]
	public static  class DlgMatchSystem
	{

		public static void RegisterUIEvent(this DlgMatch self)
		{
			self.View.E_StartMatchButton.AddListenerAsync(self.OnMatchStartBtnClickHandler);
		}

		public static void ShowWindow(this DlgMatch self, Entity contextData = null)
		{
		}

		public static async ETTask OnMatchStartBtnClickHandler(this DlgMatch self)
		{
			Tweener tween = null;
			
			try
			{
				if (self.IsMatching)
				{
					return;
				}

				self.IsMatching = true;
				self.View.E_StartMatchTextMeshProUGUI.SetText("匹配中");
				tween = self.View.E_StartMatchTextMeshProUGUI.DOText("匹配中...", 1f);
				tween.SetLoops(-1);
				
				int errorCode = await MatchHelper.StartMatch(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					self.IsMatching = false;
					tween.Kill();
					self.View.E_StartMatchTextMeshProUGUI.SetText("开始匹配");
					return;
				}
				
				tween.Kill();
				self.View.E_StartMatchTextMeshProUGUI.SetText("匹配成功");

			}
			catch (Exception e)
			{
				self.IsMatching = false;
				tween?.Kill();
				self.View.E_StartMatchTextMeshProUGUI.SetText("开始匹配");
				Log.Error(e.ToString());
			}
		}

	}
}
