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
            var playerPos = player.GetComponent<GameObjectComponent>().GameObject.transform.position;
            var selfPos = a.Crop.GetComponent<GameObjectComponent>().GameObject.transform.position;
            var dirX = selfPos.x > playerPos.x? 1 : -1;
            //先播放动画 在生成物品
            if (a.Crop.Config.HasAnimation == 1)
            {
                var animator = a.Crop.GetComponent<AnimatorComponent>();
                animator.Play(playerPos.x < selfPos.x? MotionType.FallRight : MotionType.FallLeft);

                await TimerComponent.Instance.WaitAsync(1200);//等待动画播完
            }
            
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
                    var spawnX = selfPos.x + RandomHelper.RandomNumber(-a.Crop.Config.SpawnRadius[0], a.Crop.Config.SpawnRadius[0]) * dirX;
                    var spawnY = selfPos.y + RandomHelper.RandomNumber(-a.Crop.Config.SpawnRadius[1], a.Crop.Config.SpawnRadius[1]);

                    Item item = ItemFactory.Create(a.Crop.ZoneScene().CurrentScene(), a.Crop.Config.ProducedItemIDs[i], false);
                    
                    if (a.Crop.Config.GenerateAtPlayerPos == 1)
                    {
                        var errorCode = ItemHelper.AddItemFromCurrentScene(item);
                        if (errorCode == ErrorCode.ERR_Success)
                        {
                            if (!_displayItem)
                            {
                                DisplayItem(player, item).Coroutine();
                            }

                            DeleteCropObj(a.Crop, i, j, count);
                        }
                    }
                    else
                    {
                        Game.EventSystem.Publish(new AfterItemCreate()
                        {
                            Item = item,
                            UsePos = true,
                            Bounced = true,
                            X = spawnX,
                            Y = spawnY,
                            StartX = selfPos.x,
                            StartY = selfPos.y
                        });
                        DeleteCropObj(a.Crop, i, j, count);
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

        private void DeleteCropObj(Crop crop, int i, int j, int count)
        {
            //如果是最后一个生成物 并不可重复生长 删除Crop的Obj
            if (i == crop.Config.ProducedItemIDs.Length - 1 && j == count - 1)
            {
                var tile = crop.GetParent<GridTile>();
                tile.DaysSinceLastHarvest++;
                //可重复生长
                if (crop.Config.DaysToRegrow > 0 && tile.DaysSinceLastHarvest < crop.Config.RegrowTimes)
                {
                    tile.GrowthDays = crop.Config.TotalGrowthDays - crop.Config.DaysToRegrow;
                    crop.GetComponent<CropViewComponent>().Init().Coroutine();
                }
                else
                {
                    //生成转换物体
                    if (crop.Config.TransferItemId != 0)
                    {
                        tile.AddCrop(crop.Config.TransferItemId);
                    }
                    else
                    {
                        tile.Crop = null;
                    }
                    
                    tile.DaysSinceLastHarvest = -1;
                    tile.DaysSinceDug = 0; //回到第一次挖洞的时候
                    crop.Dispose();
                }
            }
        }
    }
}