using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using System.Linq;
using BM;
using HybridCLR;
using Object = UnityEngine.Object;

namespace ET
{
	public class CodeLoader: IDisposable
	{
		public static CodeLoader Instance = new CodeLoader();

		public Action Update;
		public Action LateUpdate;
		public Action OnApplicationQuit;

		public Assembly assembly;
		
		public CodeMode CodeMode { get; set; }
		
		// 所有mono的类型
		private readonly Dictionary<string, Type> monoTypes = new Dictionary<string, Type>();
		
		// 热更层的类型
		private readonly Dictionary<string, Type> hotfixTypes = new Dictionary<string, Type>();

		private CodeLoader()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly ass in assemblies)
			{
				foreach (Type type in ass.GetTypes())
				{
					this.monoTypes[type.FullName] = type;
					this.monoTypes[type.AssemblyQualifiedName] = type;
				}
			}
		}
		
		public Type GetMonoType(string fullName)
		{
			this.monoTypes.TryGetValue(fullName, out Type type);
			return type;
		}
		
		public Type GetHotfixType(string fullName)
		{
			this.hotfixTypes.TryGetValue(fullName, out Type type);
			return type;
		}

		public void Dispose()
		{
			
		}
		
		public void Start()
		{
			this.Initialize().Coroutine();
		}

		private async ETTask Initialize()
		{
			await this.CheckHotfix();
			await this.LoadGameDll();
		}

		private async ETTask CheckHotfix()
		{
			AssetComponentConfig.DefaultBundlePackageName = "ResBundles";
			
			//重新配置热更路径(开发方便用, 打包移动端需要注释注释)
			//AssetComponentConfig.HotfixPath = Application.dataPath + "/../HotfixBundles/";
			
			Dictionary<string, bool> updatePackageBundle = new Dictionary<string, bool>()
			{
				{"Code", false},
				{"ResBundles", false},
				{"Config", false},
			};
			UpdateBundleDataInfo updateBundleDataInfo = await AssetComponent.CheckAllBundlePackageUpdate(updatePackageBundle);
			if (updateBundleDataInfo.NeedUpdate)
			{
				Debug.LogError("需要更新, 大小: " + updateBundleDataInfo.NeedUpdateSize);
				await AssetComponent.DownLoadUpdate(updateBundleDataInfo);
			}
			await AssetComponent.Initialize("Code");
			await AssetComponent.Initialize("ResBundles");
		}
		
		private async ETTask LoadGameDll()
		{
			switch (this.CodeMode)
			{
				case CodeMode.Mono:
				case CodeMode.Wolong:
				{
					byte[] assBytes = (await AssetComponent.LoadAsync<TextAsset>(BPath.Assets_Bundles_Code_Code__dll__bytes,"Code")).bytes;
					
					byte[] pdbBytes = (await AssetComponent.LoadAsync<TextAsset>(BPath.Assets_Bundles_Code_Code__pdb__bytes,"Code")).bytes;

#if UNITY_EDITOR
					AssetComponent.UnInitialize("Code");
#endif
					
					assembly = Assembly.Load(assBytes, pdbBytes);
					
#if !UNITY_EDITOR
					LoadMetadataForAOTAssembly();
#endif
					
					foreach (Type type in this.assembly.GetTypes())
					{
						this.monoTypes[type.FullName] = type;
						this.hotfixTypes[type.FullName] = type;
					}
					// IStaticMethod start = new MonoStaticMethod(assembly, "ET.Entry", "Start");
					// start.Run();
					// var startPrefab = await AssetComponent.LoadAsync<GameObject>(BPath.Assets_Bundles_ResBundles_StartPrefab__prefab);
					// GameObject instiateObj = Object.Instantiate(startPrefab);
					Type entryType = this.assembly.GetType("ET.Entry");
					IEntry entry = (IEntry)Activator.CreateInstance(entryType);
					entry.Start();

					break;
				}
				case CodeMode.Reload:
				{
					byte[] assBytes = File.ReadAllBytes(Path.Combine(Define.BuildOutputDir, "Data.dll"));
					byte[] pdbBytes = File.ReadAllBytes(Path.Combine(Define.BuildOutputDir, "Data.pdb"));
					
					assembly = Assembly.Load(assBytes, pdbBytes);
					this.LoadLogic();
					IStaticMethod start = new MonoStaticMethod(assembly, "ET.Entry", "Start");
					start.Run();
					break;
				}
			}
		}

		// 热重载调用下面三个方法
		// CodeLoader.Instance.LoadLogic();
		// Game.EventSystem.Add(CodeLoader.Instance.GetTypes());
		// Game.EventSystem.Load();
		public void LoadLogic()
		{
			if (this.CodeMode != CodeMode.Reload)
			{
				throw new Exception("CodeMode != Reload!");
			}
			
			// 傻屌Unity在这里搞了个傻逼优化，认为同一个路径的dll，返回的程序集就一样。所以这里每次编译都要随机名字
			string[] logicFiles = Directory.GetFiles(Define.BuildOutputDir, "Logic_*.dll");
			if (logicFiles.Length != 1)
			{
				throw new Exception("Logic dll count != 1");
			}

			string logicName = Path.GetFileNameWithoutExtension(logicFiles[0]);
			byte[] assBytes = File.ReadAllBytes(Path.Combine(Define.BuildOutputDir, $"{logicName}.dll"));
			byte[] pdbBytes = File.ReadAllBytes(Path.Combine(Define.BuildOutputDir, $"{logicName}.pdb"));

			Assembly hotfixAssembly = Assembly.Load(assBytes, pdbBytes);
			
			foreach (Type type in this.assembly.GetTypes())
			{
				this.monoTypes[type.FullName] = type;
				this.hotfixTypes[type.FullName] = type;
			}
			
			foreach (Type type in hotfixAssembly.GetTypes())
			{
				this.monoTypes[type.FullName] = type;
				this.hotfixTypes[type.FullName] = type;
			}
		}

		public Dictionary<string, Type> GetHotfixTypes()
		{
			return this.hotfixTypes;
		}
		
		/// <summary>
		/// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
		/// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
		/// </summary>
		public static unsafe void LoadMetadataForAOTAssembly()
		{
			// 可以加载任意aot assembly的对应的dll。但要求dll必须与unity build过程中生成的裁剪后的dll一致，而不能直接使用原始dll。
			// 我们在BuildProcessor_xxx里添加了处理代码，这些裁剪后的dll在打包时自动被复制到 {项目目录}/HybridCLRData/AssembliesPostIl2CppStrip/{Target} 目录。

			// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。	
			// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误

			HomologousImageMode mode = HomologousImageMode.SuperSet;
			
			foreach (var aotDllName in Define.DllAotList)
			{
				byte[] dllBytes = AssetComponent.Load<TextAsset>(aotDllName, "Code").bytes;

				// 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
				LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, mode);
				Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. ret:{err}");
			}
			
			AssetComponent.UnInitialize("Code");
		}
	}
}