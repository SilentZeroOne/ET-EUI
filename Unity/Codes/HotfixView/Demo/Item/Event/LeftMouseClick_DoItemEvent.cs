using ET.EventType;

namespace ET
{
    public class LeftMouseClick_DoItemEvent : AEvent<LeftMouseClick>
    {
        protected override void Run(LeftMouseClick a)
        {
            RunAsync(a).Coroutine();
        }

        private async ETTask RunAsync(LeftMouseClick a)
        {
            if (a.Item != null)
            {
                switch ((ItemType)a.Item.Config.ItemType)
                {
                    case ItemType.Commodity://丢弃物品

                        //从Inventory中移除 但不要dispose
                        InventoryComponent inventoryComponent = a.ZoneScene.GetComponent<InventoryComponent>();
                        inventoryComponent.RemoveItem(a.Item, false);

                        //加入CurrentScene的Items中
                        ItemsComponent itemsComponent = a.ZoneScene.CurrentScene().GetComponent<ItemsComponent>();
                        itemsComponent.AddItem(a.Item);

                        Game.EventSystem.Publish(new AfterItemCreate()
                        {
                            Item = a.Item,
                            UsePos = true,
                            X = a.X,
                            Y = a.Y,
                            SaveInScene = true,
                            Bounced = true
                        });

                        await TimerComponent.Instance.WaitAsync(100);

                        a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgMain>().Refresh().Coroutine();

                        break;
                }
            }
        }
    }
}