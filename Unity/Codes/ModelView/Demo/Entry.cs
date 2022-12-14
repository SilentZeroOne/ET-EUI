using System;
using System.Collections.Generic;
using BM;
using UnityEngine;

namespace ET
{
	public class Entry : IEntry
	{
		public Entry()
		{
			
		}
		
		public void Start()
		{
			try
			{
				CodeLoader.Instance.Update += Game.Update;
				CodeLoader.Instance.LateUpdate += Game.LateUpdate;
				CodeLoader.Instance.OnApplicationQuit += Game.Close;
				
				
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