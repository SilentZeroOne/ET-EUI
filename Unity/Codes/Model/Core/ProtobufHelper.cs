using System;
using System.Collections.Generic;
#if NOT_UNITY
using System.ComponentModel;
#endif
using System.IO;
using ProtoBuf;
using ProtoBuf.Meta;

namespace ET
{
    public static class ProtobufHelper
    {
	    public static void Init()
        {
        }

        public static object FromBytes(Type type, byte[] bytes, int index, int count)
        {
	        using (MemoryStream stream = new MemoryStream(bytes, index, count))
	        {
		        object o = RuntimeTypeModel.Default.Deserialize(stream, null, type);
		        if (o is ISupportInitialize supportInitialize)
		        {
			        supportInitialize.EndInit();
		        }
		        return o;
	        }
        }

        public static byte[] ToBytes(object message)
        {
	        using (MemoryStream stream = new MemoryStream())
	        {
		        ProtoBuf.Serializer.Serialize(stream, message);
		        return stream.ToArray();
	        }
        }

        public static void ToStream(object message, MemoryStream stream)
        {
            ProtoBuf.Serializer.Serialize(stream, message);
        }

        public static object FromStream(Type type, MemoryStream stream)
        {
	        object o = RuntimeTypeModel.Default.Deserialize(stream, null, type);
	        if (o is ISupportInitialize supportInitialize)
	        {
		        supportInitialize.EndInit();
	        }
	        return o;
        }
        
        public static void SaveTo(object entity, string path)
        {
	        if (string.IsNullOrEmpty(path))
	        {
		        Log.Error("Save path is Null!");
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

        public static T Deserialize<T>(string path) where T : class
        {
	        if (string.IsNullOrEmpty(path) || !File.Exists(path))
	        {
		        Log.Error("Deserialze path is null!!!");
		        return null;
	        }
	        
	        using FileStream stream = File.OpenRead(path);
            
	        return Serializer.Deserialize<T>(stream);
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