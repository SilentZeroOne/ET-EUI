using UnityEngine;

namespace ET
{
    [System.Serializable]
    public class ItemDetails
    {
        public int ItemId;

        public string ItemName;

        public ItemType ItemType;

        public Sprite ItemIcon;

        public Sprite ItemOnWorldSprite;

        public string ItemDescription;

        public int ItemUseRadius;

        public bool CanPickedUp;

        public bool CanDropped;

        public bool CanCarried;

        public int ItemPrice;

        [Range(0,1)]
        public float SellPercentage;
    }
}