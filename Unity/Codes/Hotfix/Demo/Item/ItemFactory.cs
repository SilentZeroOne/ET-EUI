namespace ET
{
    public static class ItemFactory
    {
        public static Item Create(Scene currentScene, int id, bool createView = true)
        {
            ItemsComponent itemsComponent = currentScene.GetComponent<ItemsComponent>();
            Item item = itemsComponent.AddItem(id);

            if (createView)
                Game.EventSystem.Publish(new EventType.AfterItemCreate() { Item = item, UsePos = false, SaveInScene = true });
            return item;
        }
        
        public static Item Create(Scene currentScene, int id, float x, float y)
        {
            ItemsComponent itemsComponent = currentScene.GetComponent<ItemsComponent>();
            Item item = itemsComponent.AddItem(id);

            Game.EventSystem.Publish(new EventType.AfterItemCreate()
            {
                Item = item,
                UsePos = true,
                SaveInScene = true,
                X = x,
                Y = y
            });
            return item;
        }
        
        /// <summary>
        /// Used for read cache from load scene
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Item Create(Item item)
        {
            Game.EventSystem.Publish(new EventType.AfterItemCreate()
            {
                Item = item,
                X = item.Position.x,
                Y = item.Position.y,
                UsePos = true,
                SaveInScene = false
            });
            return item;
        }
    }
}