using System;
using System.Collections.Generic;
using BM;
using UnityEngine;

namespace ET
{
	public static class Entry
	{
		public static void Start()
		{
			try
			{
				CodeLoader.Instance.Update += Game.Update;
				CodeLoader.Instance.LateUpdate += Game.LateUpdate;
				CodeLoader.Instance.OnApplicationQuit += Game.Close;
				CodeLoader.Instance.FixedUpdate += Game.FixedUpdate;
				
				
				Game.EventSystem.Add(CodeLoader.Instance.GetHotfixTypes());

				
				Game.EventSystem.Publish(new EventType.AppStart());
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}
	}
}