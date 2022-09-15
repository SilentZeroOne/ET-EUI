using ET.EventType;
using UnityEngine;

namespace ET
{
    [FriendClassAttribute(typeof(ET.GridMapManageComponent))]
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
                var gridMapManage = a.ZoneScene.CurrentScene().GetComponent<GridMapManageComponent>();
                var cellPos = gridMapManage.CurrentGrid.WorldToCell(new Vector3(a.X, a.Y, 0));
                TileDetails currentTile = gridMapManage.GetTileDetails($"{cellPos.x}x{cellPos.y}y{a.ZoneScene.CurrentScene().Name}");

                //WORKFLOW: 添加对应Type的Item使用
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
                    case ItemType.HoeTool://挖坑
                        currentTile.CanDig = false;
                        currentTile.DaysSinceDug = 0;
                        currentTile.CanDropItem = false;
                        gridMapManage.SetDigTile(currentTile);
                        //TODO:音效
                        break;
                    case ItemType.WaterTool://浇水
                        currentTile.DaysSinceWatered = 0;
                        gridMapManage.SetWaterTile(currentTile);
                        //TODO:音效
                        break;
                }
            }
        }
    }
}