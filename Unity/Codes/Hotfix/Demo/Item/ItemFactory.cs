namespace ET
{
    public static class ItemFactory
    {
        public static Item Create(Scene currentScene, int id)
        {
            ItemsComponent itemsComponent = currentScene.GetComponent<ItemsComponent>();
            Item item = itemsComponent.AddChild<Item, int>(id);

            Game.EventSystem.Publish(new EventType.AfterItemCreate() { Item = item });
            return item;
        }
    }
}