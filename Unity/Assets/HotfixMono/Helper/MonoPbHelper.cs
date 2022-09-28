using System.IO;
using ProtoBuf;
using UnityEngine;

namespace ET
{
    public static class MonoPbHelper
    {
        public static void SaveTo(object entity, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("Save path is Null!");
                return;
            }

            DirectoryInfo df = new DirectoryInfo(path);
            if (!df.Parent.Exists)
            {
                df.Parent.Create();
            }
	        
            if (File.Exists(path))
            {
                File.Delete(path);
            }
	        
            using FileStream stream = File.Create(path);
            Serializer.Serialize(stream, entity);
        }
        
        public static T Deserialize<T>(byte[] bytes) where T : class
        {
            if (bytes == null || bytes.Length == 0)
            {
                //Log.Error("Deserialze data is null!!!");
                return null;
            }

            using MemoryStream stream = new MemoryStream(bytes);
            
            return Serializer.Deserialize<T>(stream);
        }
    }
}