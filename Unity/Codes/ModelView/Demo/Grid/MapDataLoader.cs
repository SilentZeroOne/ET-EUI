using System.Collections.Generic;
using BM;
using UnityEngine;

namespace ET
{
    public class MapDataLoader: IMapDataLoader
    {
        public void GetSceneMapDataBytes(List<byte[]> output,string sceneName)
        {
            foreach (var fieldInfo in typeof(BPath).GetFields())
            {
                if (fieldInfo.Name.Contains($"MapData_{sceneName}"))
                {
                    var text = AssetComponent.Load<TextAsset>((string)fieldInfo.GetValue(null),"MapData");
                    output.Add(text.bytes);
                }
            }
        }
    }
}