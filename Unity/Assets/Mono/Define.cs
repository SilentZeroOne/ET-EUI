using System.Collections.Generic;
using BM;
using UnityEngine.Networking;

namespace ET
{
	public static class Define
	{
		public const string BuildOutputDir = "./Temp/Bin/Debug";

		public static List<string> RawDllAotList = new List<string>()
		{
			"mscorlib.dll", 
			"System.dll", 
			"System.Core.dll",
			"Unity.Mono.dll"
		};

		public static List<string> DllAotList = new List<string>() { 
			$"{BPath.Assets_Bundles_Code_mscorlib__dll__bytes}", 
			$"{BPath.Assets_Bundles_Code_System__dll__bytes}", 
			$"{BPath.Assets_Bundles_Code_System__Core__dll__bytes}",
			$"{BPath.Assets_Bundles_Code_Unity__Mono__dll__bytes}"
		};

#if UNITY_EDITOR && !ASYNC
		public static bool IsAsync = false;
#else
        public static bool IsAsync = true;
#endif
		
#if UNITY_EDITOR
		public static bool IsEditor = true;
#else
        public static bool IsEditor = false;
#endif
		
		public static UnityEngine.Object LoadAssetAtPath(string s)
		{
#if UNITY_EDITOR	
			return UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(s);
#else
			return null;
#endif
		}
		
		public static string[] GetAssetPathsFromAssetBundle(string assetBundleName)
		{
#if UNITY_EDITOR	
			return UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName);
#else
			return new string[0];
#endif
		}
		
		public static string[] GetAssetBundleDependencies(string assetBundleName, bool v)
		{
#if UNITY_EDITOR	
			return UnityEditor.AssetDatabase.GetAssetBundleDependencies(assetBundleName, v);
#else
			return new string[0];
#endif
		}
	}
}