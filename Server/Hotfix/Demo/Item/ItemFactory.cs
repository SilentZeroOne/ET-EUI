using System;

namespace ET
{
    public static class ItemFactory
    {
        public static Item Create(Entity parent, int configId)
        {
            if (!ItemConfigCategory.Instance.Contain(configId))
            {
                Log.Error($"当前创建的item不存在 id:{configId}");
                return null;
            }

            Item item = parent.AddChild<Item, int>(configId);
            item.RandomQuality();
            item.AddComponentByItemType();

            return item;
        }

        public static void AddComponentByItemType(this Item item)
        {
            switch ((ItemType)item.Config.Type)
            {
                case ItemType.Weapon:
                case ItemType.Armor:
                case ItemType.Ring:
                    
                    break;
                case ItemType.Prop:
                    
                    break;
            }
        }
    }
}