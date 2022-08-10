﻿using BM;

namespace ET
{
    public static class BMHelper
    {
        public static string StringToAB(this string self)
        {
            var replace = self.Replace("@", "_");
            foreach (var fieldInfo in typeof (BPath).GetFields())
            {
                if (fieldInfo.Name.Contains(replace))
                {
                    return (string)fieldInfo.GetValue(null);
                }
            }

            return "";
        }
        
        public static string StringToAB1(this string self)
        {
            var replace = self.Replace("@", "_");
            foreach (var fieldInfo in typeof (BPath).GetFields())
            {
                if (fieldInfo.Name.Contains(replace))
                {
                    return (string)fieldInfo.GetValue(null);
                }
            }

            return "";
        }

        public static string SceneNameToAB(this string self)
        {
            foreach (var fieldInfo in typeof (BPath).GetFields())
            {
                if (fieldInfo.Name.Contains(self) && fieldInfo.Name.EndsWith("unity"))
                {
                    return (string)fieldInfo.GetValue(null);
                }
            }

            return "";
        }
    }
}