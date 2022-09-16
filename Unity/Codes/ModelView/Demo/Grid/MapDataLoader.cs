using System.Collections.Generic;
using System.IO;
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

        public async ETTask<SavedMapData> GetSceneSavedMapData(string sceneName)
        {
            var path = Path.Combine(PathHelper.SavingPath, $"{sceneName}_MapData.sav");
            byte[] data = await FileReadHelper.DownloadData(path);
            if (data == null)
            {
                return null;
            }

            SavedMapData mapData = ProtobufHelper.Deserialize<SavedMapData>(data);
            return mapData;
        }
    }
}