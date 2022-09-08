namespace ET
{
    public static class ItemFactory
    {
        public static Item Create(Scene currentScene, int id)
        {
            ItemsComponent itemsComponent = currentScene.GetComponent<ItemsComponent>();
            Item item = itemsComponent.AddItem(id);

            Game.EventSystem.Publish(new EventType.AfterItemCreate() { Item = item ,UsePos = false,SaveInScene = true});
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