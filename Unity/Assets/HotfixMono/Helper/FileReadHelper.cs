using System;
using UnityEngine.Networking;

namespace ET
{
    public static class FileReadHelper
    {
        public static async ETTask<byte[]> DownloadData(string url)
        {
            var uri = new Uri(url);
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                await webRequest.SendWebRequest();
#if UNITY_2020_1_OR_NEWER
                if (webRequest.result != UnityWebRequest.Result.Success)
#else
                if (!string.IsNullOrEmpty(webRequest.error))
#endif
                {
                    Log.Debug("下载Bundle失败 重试\n" + webRequest.error + "\nURL：" + url);
                    return null;
                }
                return webRequest.downloadHandler.data;
            }
        }
    }
}