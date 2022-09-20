﻿using ET.EventType;
using UnityEngine;

namespace ET
{
    [FriendClassAttribute(typeof(ET.GridMapManageComponent))]
    [FriendClassAttribute(typeof(ET.Unit))]
    [FriendClassAttribute(typeof(ET.GridTile))]
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
                GridTile currentTile = gridMapManage.GetGridTile(cellPos.x, cellPos.y);

                Unit player = UnitHelper.GetMyUnitFromCurrentScene(a.ZoneScene.CurrentScene());

                if ((ItemType)a.Item.Config.ItemType != ItemType.Seed || (ItemType)a.Item.Config.ItemType != ItemType.Commodity ||
                    (ItemType)a.Item.Config.ItemType != ItemType.Furniture || (ItemType)a.Item.Config.ItemType != ItemType.ReapableScenary)
                {
                    if (player.UseTool) return;

                    //给Animator设置XY
                    player.GetComponent<AnimatorComponent>().SetMouseParmas(a.X, a.Y);
                }

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
                        player.UseTool = true;
                        //播放动画
                        var toolConfig = AnimatorControllerConfigCategory.Instance.GetConfigByNameAndStatus(AnimatorType.Tool.ToString(), (int)AnimatorStatus.Hoe);
                        player.GetComponent<AnimatorComponent>().OverrideAnimator(AnimatorType.Tool, toolConfig.OverrideControllerName);
                        player.GetComponent<AnimatorComponent>().Play(MotionType.UseTool);

                        //动画总长度 800  450时 正好挖下去
                        await TimerComponent.Instance.WaitAsync(450);

                        //TODO:音效
                        currentTile.CanDig = false;
                        currentTile.DaysSinceDug = 0;
                        currentTile.CanDropItem = false;
                        gridMapManage.SetDigTile(currentTile);
                        //gridMapManage.SaveMapData();
                        await TimerComponent.Instance.WaitAsync(350);
                        //动画结束
                        player.GetComponent<AnimatorComponent>().ForEveryAnimator(AnimatorControlType.ResetTrigger, MotionType.UseTool.ToString());

                        player.UseTool = false;
                        break;
                    case ItemType.WaterTool://浇水
                        player.UseTool = true;
                        var armConfig = AnimatorControllerConfigCategory.Instance.GetConfigByNameAndStatus(AnimatorType.Arm.ToString(), (int)AnimatorStatus.Water);
                        var bodyConfig = AnimatorControllerConfigCategory.Instance.GetConfigByNameAndStatus(AnimatorType.Body.ToString(), (int)AnimatorStatus.Water);
                        var hairConfig = AnimatorControllerConfigCategory.Instance.GetConfigByNameAndStatus(AnimatorType.Hair.ToString(), (int)AnimatorStatus.Water);
                        toolConfig = AnimatorControllerConfigCategory.Instance.GetConfigByNameAndStatus(AnimatorType.Tool.ToString(), (int)AnimatorStatus.Water);

                        player.GetComponent<AnimatorComponent>().OverrideAnimator(AnimatorType.Tool, toolConfig.OverrideControllerName);
                        player.GetComponent<AnimatorComponent>().OverrideAnimator(AnimatorType.Arm, armConfig.OverrideControllerName);
                        player.GetComponent<AnimatorComponent>().OverrideAnimator(AnimatorType.Body, bodyConfig.OverrideControllerName);
                        player.GetComponent<AnimatorComponent>().OverrideAnimator(AnimatorType.Hair, hairConfig.OverrideControllerName);

                        player.GetComponent<AnimatorComponent>().Play(MotionType.UseTool);

                        //动画总长度 800  300时 正好开始浇水
                        await TimerComponent.Instance.WaitAsync(300);

                        //TODO:音效
                        currentTile.DaysSinceWatered = 0;
                        gridMapManage.SetWaterTile(currentTile);
                        //gridMapManage.SaveMapData();
                        await TimerComponent.Instance.WaitAsync(500);
                        //动画结束
                        player.GetComponent<AnimatorComponent>().ForEveryAnimator(AnimatorControlType.ResetTrigger, MotionType.UseTool.ToString());

                        player.UseTool = false;
                        break;
                }
            }
        }
    }
}