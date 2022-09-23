using ET.EventType;
using UnityEngine;

namespace ET
{
    [FriendClassAttribute(typeof(ET.GridTile))]
    public class HarvestCrop_SpawnItem : AEvent<HarvestCrop>
    {
        protected override void Run(HarvestCrop a)
        {
            RunAsync(a).Coroutine();
        }

        private bool _displayItem;
        private async ETTask RunAsync(HarvestCrop a)
        {
            Unit player = UnitHelper.GetMyUnitFromCurrentScene(a.ZoneScene.CurrentScene());
            for (int i = 0; i < a.Crop.Config.ProducedItemIDs.Length; i++)
            {
                int count;
                if (a.Crop.Config.ProducedItemMinAmount[i] == a.Crop.Config.ProducedItemMaxAmount[i])
                {
                    count = a.Crop.Config.ProducedItemMinAmount[i];
                }
                else
                {
                    count = RandomHelper.RandomNumber(a.Crop.Config.ProducedItemMinAmount[i], a.Crop.Config.ProducedItemMaxAmount[i] + 1);
                }

                for (int j = 0; j < count; j++)
                {
                    Item item = ItemFactory.Create(a.Crop.ZoneScene().CurrentScene(), a.Crop.Config.ProducedItemIDs[i], false);
                    var errorCode = ItemHelper.AddItemFromCurrentScene(item);
                    if (errorCode == ErrorCode.ERR_Success)
                    {
                        if (!_displayItem)
                        {
                            DisplayItem(player, item).Coroutine();
                        }

                        //如果是最后一个生成物 并不可重复生长 删除Crop的Obj
                        if (i == a.Crop.Config.ProducedItemIDs.Length - 1 && j == count - 1)
                        {
                            var tile = a.Crop.GetParent<GridTile>();
                            tile.DaysSinceLastHarvest++;
                            //可重复生长
                            if (a.Crop.Config.DaysToRegrow > 0 && tile.DaysSinceLastHarvest < a.Crop.Config.RegrowTimes)
                            {
                                tile.GrowthDays = a.Crop.Config.TotalGrowthDays - a.Crop.Config.DaysToRegrow;
                                a.Crop.GetComponent<CropViewComponent>().Init().Coroutine();
                            }
                            else
                            {
                                UnityEngine.Object.Destroy(a.Crop.GetComponent<GameObjectComponent>().GameObject);
                                tile.Crop = null;
                                tile.DaysSinceLastHarvest = -1;
                                tile.DaysSinceDug = 0; //回到第一次挖洞的时候
                                a.Crop.Dispose();
                            }
                        }
                    }
                    else
                    {
                        item.Dispose();
                        //TODO: 记录没有成功加入背包的东西
                        Log.Error(errorCode.ToString());
                        return;
                    }
                }
            }

            await ETTask.CompletedTask;
        }

        private async ETTask DisplayItem(Unit player, Item item)
        {
            _displayItem = true;
            //显示出东西在头顶
            var holdItemSprite = player.GetComponent<GameObjectComponent>().GameObject.GetComponentFormRC<SpriteRenderer>("HoldItem");
            holdItemSprite.color = new Color(1, 1, 1, 1);
            holdItemSprite.sprite = IconHelper.LoadIconSprite(item.Config.ItemOnWorldSprite);
            //隐藏
            await TimerComponent.Instance.WaitAsync(600);
            holdItemSprite.color = new Color(1, 1, 1, 0);

            _displayItem = false;
        }
    }
}