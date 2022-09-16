using System.Collections.Generic;

namespace ET
{
    public interface IMapDataLoader
    {
        void GetSceneMapDataBytes(List<byte[]> output, string sceneName);
        ETTask<SavedMapData> GetSceneSavedMapData(string sceneName);
    }
}