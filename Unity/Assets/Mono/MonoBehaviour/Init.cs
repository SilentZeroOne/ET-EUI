﻿using System.Threading;
using BM;
using UnityEngine;

namespace ET
{
	// 1 mono模式 2 ILRuntime模式 3 mono热重载模式
	public enum CodeMode
	{
		Mono = 1,
		Wolong = 2,
		Reload = 3,
	}
	
	public class Init: MonoBehaviour
	{
		public CodeMode CodeMode = CodeMode.Mono;
		
		private void Awake()
		{
#if ENABLE_IL2CPP
			this.CodeMode = CodeMode.Wolong;
#endif
			
			System.AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
			{
				Log.Error(e.ExceptionObject.ToString());
			};
			
			SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);
			
			DontDestroyOnLoad(gameObject);
			

			ETTask.ExceptionHandler += Log.Error;

			Log.ILog = new UnityLogger();

			Options.Instance = new Options();

			CodeLoader.Instance.CodeMode = this.CodeMode;
			Options.Instance.Develop = 1;
			Options.Instance.LogLevel = 0;
		}

		private void Start()
		{
			CodeLoader.Instance.Start();
		}

		private void Update()
		{
			CodeLoader.Instance.Update?.Invoke();
			AssetComponent.Update();
		}

		private void LateUpdate()
		{
			CodeLoader.Instance.LateUpdate?.Invoke();
		}

		private void OnApplicationQuit()
		{
			CodeLoader.Instance.OnApplicationQuit?.Invoke();
			CodeLoader.Instance.Dispose();
		}
	}
}