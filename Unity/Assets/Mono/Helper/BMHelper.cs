using BM;

namespace ET
{
    public static class BMHelper
    {
        public static string StringToAB(this string self)
        {
            foreach (var fieldInfo in typeof (BPath).GetFields())
            {
                if (fieldInfo.Name.Contains(self))
                {
                    return (string)fieldInfo.GetValue(null);
                }
            }

            return "";
        }
    }
}