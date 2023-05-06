using BM;
using UnityEngine;
using UnityEngine.U2D;

namespace ET
{
    [FriendClassAttribute(typeof(ET.Card))]
    public static partial class CardHelper
    {
        public const string AtlasName = BPath.Assets_Bundles_ResBundles_Atlas_Pokers__spriteatlas;
        public static Sprite GetCardSprite(Card card)
        {
            string spriteName;
            if (card.CardSuit != (int)Suits.None)
                spriteName = $"{((Suits)card.CardSuit).ToString()}{((Weight)card.CardWeight).ToString()}";
            else
                spriteName = $"{((Weight)card.CardWeight).ToString()}";
            
            return GetCardSprite(spriteName);
        }

        public static Sprite GetCardSprite(string spriteName)
        {
            var atlas = AssetComponent.Load<SpriteAtlas>(AtlasName);
            return atlas.GetSprite(spriteName);
        }
    }
}