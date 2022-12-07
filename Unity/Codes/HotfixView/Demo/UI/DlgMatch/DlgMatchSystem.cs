using System.Collections;
using System.Collections.Generic;
using System;
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
			try
			{
				if (self.IsMatching)
				{
					return;
				}

				self.IsMatching = true;
				
				int errorCode = await MatchHelper.StartMatch(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					self.IsMatching = false;
					return;
				}
				
				
				
			}
			catch (Exception e)
			{
				self.IsMatching = false;
				Log.Error(e.ToString());
			}
		}

	}
}
