namespace ET
{
    public static class ItemFactory
    {
        public static Item Create(Scene currentScene, int id)
        {
            ItemsComponent itemsComponent = currentScene.GetComponent<ItemsComponent>();
            Item item = itemsComponent.AddChild<Item, int>(id);

            Game.EventSystem.Publish(new EventType.AfterItemCreate() { Item = item ,UsePos = false});
            return item;
        }
        
        public static Item Create(Scene currentScene, int id, float x, float y)
        {
            ItemsComponent itemsComponent = currentScene.GetComponent<ItemsComponent>();
            Item item = itemsComponent.AddChild<Item, int>(id);

            Game.EventSystem.Publish(new EventType.AfterItemCreate()
            {
                Item = item,
                X = x,
                Y = y,
                UsePos = true
            });
            return item;
        }
    }
}