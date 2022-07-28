using UnityEngine;

namespace ET
{
    public static class PingViewHelper
    {
        public static Color PingColor(long value)
        {
            if (value <= 150)
            {
                return Color.green;
            }
            else if (value <= 300)
            {
                return Color.yellow;
            }
            else
            {
                return Color.red;
            }
        }
    }
}