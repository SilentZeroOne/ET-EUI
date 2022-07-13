using System;
using UnityEngine;

namespace ET
{
    [FriendClass(typeof(Item))]
    public static class ItemViewHelper
    {
        public static Color ItemQulityColor(this Item self)
        {
            ItemQuality quality = (ItemQuality)self.Quality;
            switch (quality)
            {
                case ItemQuality.General:
                    return Color.white;
                case ItemQuality.Good:
                    return Color.green;
                case ItemQuality.Excellent:
                    return Color.blue;
                case ItemQuality.Epic:
                    return Color.magenta;
                case ItemQuality.Legand:
                    return new Color(225.0f / 255, 128.0f / 255, 0);
                default:
                    return Color.black;
            }
        }
    }
}