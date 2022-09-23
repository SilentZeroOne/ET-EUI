using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UEventSystem = UnityEngine.EventSystems.EventSystem;

namespace ET
{
	public class DlgCursorUpdateSystem: UpdateSystem<DlgCursor>
	{
		public override void Update(DlgCursor self)
		{
            if (Cursor.visible)
            {
                Cursor.visible = false;
            }
			self.SyncPosition();
            if (!self.InteractWithUI())
            {
                self.SetCursor(self.CurrentCursor);
                self.CheckCursorVaild();
                self.CheckPlayerLeftMouseInput();
            }
            else
            {
                self.SetCursor(self.DefaultCursor);
            }
		}
	}

    [FriendClass(typeof(DlgCursor))]
    [FriendClassAttribute(typeof(ET.DlgCursorViewComponent))]
    [FriendClassAttribute(typeof(ET.GridMapManageComponent))]
    [FriendClassAttribute(typeof(ET.Item))]
    [FriendClassAttribute(typeof(ET.GridTile))]
    public static class DlgCursorSystem
    {

        public static void RegisterUIEvent(this DlgCursor self)
        {

        }

        public static void ShowWindow(this DlgCursor self, Entity contextData = null)
        {
            Cursor.visible = false;
            self.GetDefaultCursor();
            self.SetCursorImage();
        }

        private static void GetDefaultCursor(this DlgCursor self)
        {
            var config = CursorConfigCategory.Instance.GetDefaultCursor();
            if (config != null)
            {
                self.DefaultCursor = IconHelper.LoadIconSprite(config.CursorImage);
            }
        }

        public static void SetCursorImage(this DlgCursor self, Item item = null)
        {
            self.CurrentItem = item;

            CursorConfig config = item == null ? CursorConfigCategory.Instance.GetDefaultCursor() :
                    CursorConfigCategory.Instance.GetCursorConfigByItemType(item.Config.ItemType);

            if (config != null)
            {
                self.CurrentCursor = IconHelper.LoadIconSprite(config.CursorImage);
            }
        }

        public static void SetCursor(this DlgCursor self, Sprite sprite)
        {
            if (self.View.E_CursorImage.sprite != sprite)
            {
                self.View.E_CursorImage.sprite = sprite;
            }

            self.CursorEnable = true;
        }

        public static void SyncPosition(this DlgCursor self)
        {
            var pos = InputHelper.GetMousePosition();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(self.View.uiRectTransform, pos,
                GlobalComponent.Instance.UICamera, out pos);

            self.View.E_CursorImage.transform.localPosition = pos;
        }

        public static void CheckCursorVaild(this DlgCursor self)
        {
            if (self.CurrentItem == null) return;

            var currentScene = self.ZoneScene().CurrentScene();
            var gridMapManage = currentScene.GetComponent<GridMapManageComponent>();
            if (gridMapManage != null && gridMapManage.MapDataLoaded && gridMapManage.CurrentGrid != null)
            {
                var inputPos = InputHelper.GetMousePosition();
                var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(inputPos.x, inputPos.y, -Camera.main.gameObject.transform.position.z));
                var cellPos = gridMapManage.CurrentGrid.WorldToCell(worldPos);

                var playerGridPos = gridMapManage.CurrentGrid.WorldToCell(UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene())
                        .GetComponent<GameObjectComponent>().GameObject.transform.position);

                if (Mathf.Abs(cellPos.x - playerGridPos.x) > self.CurrentItem.Config.ItemUseRadius ||
                    Mathf.Abs(cellPos.y - playerGridPos.y) > self.CurrentItem.Config.ItemUseRadius)
                {
                    self.CursorEnable = false;
                    return;
                }

                GridTile currentTile = gridMapManage.GetGridTile(cellPos.x, cellPos.y);
                if (currentTile != null)
                {
                    //WORKFLOW:添加其他类型的Item case
                    switch ((ItemType)self.CurrentItem.Config.ItemType)
                    {
                        case ItemType.Commodity:
                            self.CursorEnable = self.CurrentItem.Config.CanDropped == 1 && currentTile.CanDropItem;
                            break;
                        case ItemType.Seed:
                            self.CursorEnable = currentTile.DaysSinceDug != -1 && currentTile.Crop == null;
                            break;
                        case ItemType.HoeTool:
                            self.CursorEnable = currentTile.CanDig;
                            break;
                        case ItemType.WaterTool:
                            self.CursorEnable = currentTile.DaysSinceDug != -1 && currentTile.DaysSinceWatered == -1;
                            break;
                        case ItemType.CollectionTool:
                            self.CursorEnable = currentTile.Crop != null && currentTile.GrowthDays >= currentTile.Crop.Config.TotalGrowthDays;
                            break;
                    }
                }
                else
                {
                    self.CursorEnable = false;
                }
            }
        }

        public static void CheckPlayerLeftMouseInput(this DlgCursor self)
        {
            if (self.CurrentItem != null && self.CursorEnable && InputHelper.GetMouseButtonDown(0))
            {
                var inputPos = InputHelper.GetMousePosition();
                var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(inputPos.x, inputPos.y, -Camera.main.gameObject.transform.position.z));
                Game.EventSystem.Publish(new EventType.LeftMouseClick()
                {
                    ZoneScene = self.ZoneScene(),
                    Item = self.CurrentItem,
                    X = worldPos.x,
                    Y = worldPos.y
                });

                InventoryComponent inventoryComponent = self.ZoneScene().GetComponent<InventoryComponent>();
                var newItem = inventoryComponent.GetItemByConfigId(self.CurrentItem.ConfigId);
                if (newItem != null)
                {
                    self.CurrentItem = newItem;
                }
                else
                {
                    Game.EventSystem.Publish(new EventType.OnItemSelected() { ZoneScene = self.ZoneScene(), Item = self.CurrentItem, Carried = false });
                }
            }
        }

        public static bool InteractWithUI(this DlgCursor self)
        {
            if (UEventSystem.current != null && UEventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }

            return false;
        }

    }
}
