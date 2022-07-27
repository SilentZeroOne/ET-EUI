﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgChat))]
	[FriendClass(typeof(Scroll_Item_chat))]
	public static  class DlgChatSystem
	{

		public static void RegisterUIEvent(this DlgChat self)
		{
			self.RegisterCloseEvent<DlgChat>(self.View.E_CloseButton);
			self.View.E_SendButton.AddListenerAsync(self.OnSendButtonHandler);
			self.View.E_ChatLoopVerticalScrollRect.AddItemRefreshListener(self.OnChatItemRefreshHandler);
		}

		public static void ShowWindow(this DlgChat self, Entity contextData = null)
		{
			self.Refresh();
		}

		public static void HideWindow(this DlgChat self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemChats);
		}

		public static void Refresh(this DlgChat self)
		{
			int count = self.ZoneScene().GetComponent<ChatComponent>().GetChatMessageCount();

			self.AddUIScrollItems(ref self.ScrollItemChats, count);
			self.View.E_ChatLoopVerticalScrollRect.SetVisible(true, count);
			self.View.E_ChatLoopVerticalScrollRect.RefillCellsFromEnd();
		}
		
		public static void OnChatItemRefreshHandler(this DlgChat self, Transform transform, int index)
		{
			Scroll_Item_chat scrollItemChat = self.ScrollItemChats[index].BindTrans(transform);

			ChatInfo chatInfo = self.ZoneScene().GetComponent<ChatComponent>().GetChatMessageByIndex(index);

			scrollItemChat.E_ChatText.text = chatInfo.Message;
			scrollItemChat.E_NameText.text = chatInfo.Name + ":";
		}

		public static async ETTask OnSendButtonHandler(this DlgChat self)
		{
			try
			{
				int errCode = await ChatHelper.SendChatMessage(self.ZoneScene(), self.View.E_MessageInputField.text);
				if (errCode != ErrorCode.ERR_Success)
				{
					Log.Error(errCode.ToString());
					return;
				}

				self.View.E_MessageInputField.text = "";
				self.Refresh();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
		 

	}
}
