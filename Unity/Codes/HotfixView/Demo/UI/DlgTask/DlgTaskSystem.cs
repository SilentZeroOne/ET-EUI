using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgTask))]
	[FriendClass(typeof(TaskInfo))]
	public static  class DlgTaskSystem
	{

		public static void RegisterUIEvent(this DlgTask self)
		{
			self.RegisterCloseEvent<DlgTask>(self.View.E_CloseButton);
			self.View.E_TasksLoopVerticalScrollRect.AddItemRefreshListener(self.OnTaskItemRefreshHandler);
		}

		public static void ShowWindow(this DlgTask self, Entity contextData = null)
		{
			self.Refresh();
		}

		public static void HideWindow(this DlgTask self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemTasks);
		}

		public static void Refresh(this DlgTask self)
		{
			int count = self.ZoneScene().GetComponent<TaskComponent>().GetTaskInfoCount();
			self.AddUIScrollItems(ref self.ScrollItemTasks, count);
			self.View.E_TasksLoopVerticalScrollRect.SetVisible(true, count);
		}

		public static void OnTaskItemRefreshHandler(this DlgTask self,Transform transform, int index)
		{
			Scroll_Item_task itemTask = self.ScrollItemTasks[index].BindTrans(transform);
			TaskInfo taskInfo = self.ZoneScene().GetComponent<TaskComponent>().GetTaskInfoByIndex(index);

			TaskConfig taskConfig = TaskConfigCategory.Instance.Get(taskInfo.ConfigId);
			
			itemTask.E_TaskNameText.SetText(taskConfig.TaskName);
			itemTask.E_TaskDescText.SetText(taskConfig.TaskDesc);
			itemTask.E_TaskProgressText.SetText($"{taskInfo.TaskProgress}/{taskConfig.TaskTargetCount}");
			itemTask.E_TaskRewardText.SetText(taskConfig.TaskRewardCount.ToString());
			itemTask.E_ReceiveTipText.SetText(taskInfo.IsTaskState(TaskState.Complete)? "领取奖励" : "未完成");
			itemTask.E_ReceiveButton.interactable = taskInfo.IsTaskState(TaskState.Complete);
			itemTask.E_ReceiveButton.AddListenerAsyncWithId(self.OnReceiveRewardHandler, taskInfo.ConfigId);
		}

		public static async ETTask OnReceiveRewardHandler(this DlgTask self, int taskConfigId)
		{
			try
			{
				int errorCode = await TaskHelper.GetTaskReward(self.ZoneScene(), taskConfigId);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Debug(errorCode.ToString());
					return;
				}
				
				self.Refresh();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}



	}
}
