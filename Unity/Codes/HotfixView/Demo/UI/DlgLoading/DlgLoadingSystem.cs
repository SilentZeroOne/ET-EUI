using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[Timer(TimerType.LoadingUITimer)]
	public class LoadingUITimer: ATimer<DlgLoading>
	{
		public override void Run(DlgLoading self)
		{
			self.UpdateText();
		}
	}

	[FriendClass(typeof(DlgLoading))]
	public static  class DlgLoadingSystem
	{

		public static void RegisterUIEvent(this DlgLoading self)
		{
		 
		}

		public static void ShowWindow(this DlgLoading self, Entity contextData = null)
		{
			self.LoadingTextBuilder = new StringBuilder();
			self.LoadingTextBuilder.Append("Loading");
		}

		/// <summary>
		/// 不要浅入，直接显示
		/// </summary>
		/// <param name="self"></param>
		public static void FadeIn(this DlgLoading self)
		{
			self.View.E_BackgroundCanvasGroup.blocksRaycasts = true;
			self.View.E_BackgroundCanvasGroup.DOFade(1, 0);
			self.Timer = TimerComponent.Instance.NewRepeatedTimer(500, TimerType.LoadingUITimer, self);
		}

		/// <summary>
		/// 浅出
		/// </summary>
		/// <param name="self"></param>
		public static void FadeOut(this DlgLoading self)
		{
			self.View.E_BackgroundCanvasGroup.DOFade(0, Settings.FadeDuration);
			self.View.E_BackgroundCanvasGroup.blocksRaycasts = false;
			TimerComponent.Instance.Remove(ref self.Timer);
		}

		public static void UpdateText(this DlgLoading self)
		{
			var split = self.LoadingTextBuilder.ToString().Split(".");
			if (split.Length == 4)
			{
				self.LoadingTextBuilder.Clear();
				self.LoadingTextBuilder.Append("Loading");
			}
			else
			{
				self.LoadingTextBuilder.Append(".");
			}

			self.View.E_LoadingTextMeshProUGUI.SetText(self.LoadingTextBuilder);
		}

	}
}
