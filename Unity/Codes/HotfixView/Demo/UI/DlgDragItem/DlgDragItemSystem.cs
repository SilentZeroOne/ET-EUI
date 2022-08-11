using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgDragItem))]
	public static  class DlgDragItemSystem
	{

		public static void RegisterUIEvent(this DlgDragItem self)
		{
		 
		}

		public static void ShowWindow(this DlgDragItem self, Entity contextData = null)
		{
			Item item = contextData as Item;
			self.View.E_ItemImage.sprite = IconHelper.LoadIconSprite(item.Config.ItemIcon);
			self.View.E_ItemImage.SetNativeSize();
		}

		public static void SyncPosition(this DlgDragItem self,Vector2 pos)
		{
			self.View.E_ItemImage.transform.position = pos;
			Log.Debug(pos.ToString());
			Log.Debug(InputHelper.GetMousePosition().ToString());
		}

	}
}
