using System.IO;
using BM;
using UnityEditor;
using UnityEngine;

namespace ET
{
    public static class BuildHelper
    {
        private const string relativeDirPrefix = "../Release";

        public static string BuildFolder = "../Release/{0}/StreamingAssets/";
        
        public static string ProjectDir => Directory.GetParent(Application.dataPath).ToString();
        
        public static string HybridCLRDataDir { get; } = $"{ProjectDir}/HybridCLRData";

        public static string AssembliesPostIl2CppStripDir => $"{HybridCLRDataDir}/AssembliesPostIl2CppStrip";

        public static void Build(PlatformType type, BuildAssetBundleOptions buildAssetBundleOptions, BuildOptions buildOptions, bool isBuildExe, bool isContainAB, bool clearFolder)
        {
            AssetLoadTable assetLoadTable = AssetDatabase.LoadAssetAtPath<AssetLoadTable>(BundleMasterWindow.AssetLoadTablePath);
            BuildFolder = assetLoadTable.BundlePath;
            
            BuildTarget buildTarget = BuildTarget.StandaloneWindows;
            string programName = "ET";
            string exeName = programName;
            switch (type)
            {
                case PlatformType.PC:
                    buildTarget = BuildTarget.StandaloneWindows64;
                    exeName += ".exe";
                    break;
                case PlatformType.Android:
                    buildTarget = BuildTarget.Android;
                    exeName += ".apk";
                    break;
                case PlatformType.IOS:
                    buildTarget = BuildTarget.iOS;
                    break;
                case PlatformType.MacOS:
                    buildTarget = BuildTarget.StandaloneOSX;
                    break;
            }

            string fold = string.Format(BuildFolder, type);

            if (clearFolder && Directory.Exists(fold))
            {
                Directory.Delete(fold, true);
                Directory.CreateDirectory(fold);
            }
            else
            {
                Directory.CreateDirectory(fold);
            }

            UnityEngine.Debug.Log("开始资源打包");
            //BuildPipeline.BuildAssetBundles(fold, buildAssetBundleOptions, buildTarget);
            BuildAssets.BuildAllBundle();
            UnityEngine.Debug.Log("完成资源打包");

            if (isContainAB)
            {
                FileHelper.CleanDirectory("Assets/StreamingAssets/");
                FileHelper.CopyDirectory(fold, "Assets/StreamingAssets/");
            }

            if (isBuildExe)
            {
                AssetDatabase.Refresh();
                string[] levels = {
                    "Assets/Scenes/Init.unity",
                };
                UnityEngine.Debug.Log("开始EXE打包");
                BuildPipeline.BuildPlayer(levels, $"{relativeDirPrefix}/{exeName}", buildTarget, buildOptions);
                CopyDllToAssets(EditorUserBuildSettings.activeBuildTarget);
                UnityEngine.Debug.Log("完成exe打包");
            }
            else
            {
                if (isContainAB && type == PlatformType.PC)
                {
                    string targetPath = Path.Combine(relativeDirPrefix, $"{programName}_Data/StreamingAssets/");
                    FileHelper.CleanDirectory(targetPath);
                    Debug.Log($"src dir: {fold}    target: {targetPath}");
                    FileHelper.CopyDirectory(fold, targetPath);
                }
            }
        }
        
        /// <summary>
        /// 复制AOT Dll
        /// </summary>
        /// <param name="target"></param>
        public static void CopyDllToAssets(BuildTarget target)
        {
            string aotDllDir = $"{AssembliesPostIl2CppStripDir}/{target}";
            foreach (var dll in Define.RawDllAotList)
            {
                string dllPath = $"{aotDllDir}/{dll}";
                if(!File.Exists(dllPath))
                {
                    Debug.LogError($"ab中添加AOT补充元数据dll:{dllPath} 时发生错误,文件不存在。需要构建一次主包后才能生成裁剪后的AOT dll");
                    continue;
                }
                string dllBytesPath = $"{Application.dataPath}/Bundles/Code/{dll}.bytes";
                File.Copy(dllPath, dllBytesPath, true);
            }
            
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }
    }
}
