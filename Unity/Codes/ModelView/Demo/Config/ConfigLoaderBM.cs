using System.Collections.Generic;
using System.Reflection;
using BM;
using UnityEngine;

namespace ET
{
    public class ConfigLoaderBM : IConfigLoader
    {
        public void GetAllConfigBytes(Dictionary<string, byte[]> output)
        {
            foreach (var fieldInfo in typeof(BPath).GetFields())
            {
                if (fieldInfo.Name.Contains("Config"))
                {
                    var text = AssetComponent.Load<TextAsset>((string)fieldInfo.GetValue(null),"Config");
                    output[text.name] = text.bytes;
                }
            }
        }

        public byte[] GetOneConfigBytes(string configName)
        {
            return AssetComponent.Load<TextAsset>(configName,"Config").bytes;
        }
    }
}