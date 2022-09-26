using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(DlgMain))]
    [FriendClassAttribute(typeof(ET.ESItemSlot))]
    [FriendClassAttribute(typeof(ET.Item))]
    [FriendClassAttribute(typeof(ET.GameTimeComponent))]
    public static class DlgMainSystem
    {

        public static void RegisterUIEvent(this DlgMain self)
        {
            self.View.E_InventoryButton.AddListener(self.OnInventoryButtonClick);
        }

        public static void ShowWindow(this DlgMain self, Entity contextData = null)
        {
            self.InitSlots();
            self.RefreshAllTimeUI();
        }

        public static void RefreshAllTimeUI(this DlgMain self)
        {
            var gameTimeComponent = self.ZoneScene().GetComponent<GameTimeComponent>();
            self.RefreshDayText(gameTimeComponent);
            self.RefreshSeasonImage(gameTimeComponent);
            self.RefreshTimeImage(gameTimeComponent);
            self.RefreshSunRiseImage(gameTimeComponent);
            self.RefreshTimeUI(gameTimeComponent);
        }

        public static void OnInventoryButtonClick(this DlgMain self)
        {
            var uiComponent = self.ZoneScene().GetComponent<UIComponent>();
            bool visiable = uiComponent.IsWindowVisible(WindowID.WindowID_Inventory);
            if (visiable)
            {
                uiComponent.HideWindow(WindowID.WindowID_Inventory);
            }
            else
            {
                uiComponent.ShowWindow(WindowID.WindowID_Inventory);
            }
        }

        public static void OnSlotClick(this DlgMain self, BaseEventData evt, ESItemSlot slot, Item item)
        {
            var isSelected = slot.E_HightLightImage.gameObject.activeInHierarchy;
            slot.E_HightLightImage.gameObject.SetActive(!isSelected);
            self.CurrentItemConfigId = isSelected ? 0 : item.ConfigId;
            for (int i = 0; i < self.Slots.Count; i++)
            {
                if (i != slot.DataId)
                {
                    self.Slots[i].E_HightLightImage.SetVisible(false);
                }
            }

            Game.EventSystem.Publish(new EventType.OnItemSelected() { ZoneScene = self.ZoneScene(), Item = item, Carried = !isSelected });
        }

        public static void InitSlots(this DlgMain self)
        {
            self.Slots.Add(self.View.ESItemSlot);
            self.Slots.Add(self.View.ESItemSlot1);
            self.Slots.Add(self.View.ESItemSlot2);
            self.Slots.Add(self.View.ESItemSlot3);
            self.Slots.Add(self.View.ESItemSlot4);
            self.Slots.Add(self.View.ESItemSlot5);
            self.Slots.Add(self.View.ESItemSlot6);
            self.Slots.Add(self.View.ESItemSlot7);
            self.Slots.Add(self.View.ESItemSlot8);
            self.Slots.Add(self.View.ESItemSlot9);

            for (int i = 0; i < self.Slots.Count; i++)
            {
                self.Slots[i].uiTransform.gameObject.GetOrAddComponent<MonoBridge>().BelongToEntityId = self.Slots[i].InstanceId;
                self.Slots[i].DataId = i;
            }

            self.Refresh().Coroutine();
        }

        public static async ETTask Refresh(this DlgMain self)
        {
            InventoryComponent inventoryComponent = self.ZoneScene().GetComponent<InventoryComponent>();

            while (!inventoryComponent.Loaded)
            {
                await TimerComponent.Instance.WaitAsync(50);
            }
            
            for (int i = 0; i < self.Slots.Count; i++)
            {
                ESItemSlot slot = self.Slots[i];
                Item item = inventoryComponent.GetItemByIndex(i);
                slot.Init(item, inventoryComponent, self.CurrentItemConfigId);
                if (item != null && !item.IsDisposed)
                {
                    slot.E_ItemEventTrigger.RegisterEvent(EventTriggerType.PointerClick, (evt) => self.OnSlotClick(evt, slot, item));
                    if (slot.GetComponent<ActivityButtonComponent>() == null)
                    {
                        var code = i == self.Slots.Count - 1? -1 : i;
                        slot.AddComponent<ActivityButtonComponent, int>(code);
                    }
                }
                else
                {
                    slot.E_ItemEventTrigger.triggers.Clear();
                }
            }
        }

        /// <summary>
        /// 游戏内每秒更新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="gameTimeComponent"></param>
        public static void RefreshTimeUI(this DlgMain self, GameTimeComponent gameTimeComponent)
        {
            self.View.E_CurrentTimeTextMeshProUGUI.SetText($"{gameTimeComponent.GameHour:00}:{gameTimeComponent.GameMinute:00}:{gameTimeComponent.GameSecond:00}");
        }
        
        /// <summary>
        /// 游戏内每天更新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="gameTimeComponent"></param>
        public static void RefreshDayText(this DlgMain self, GameTimeComponent gameTimeComponent)
        {
            self.View.E_YearMonthDayTextMeshProUGUI.SetText($"{gameTimeComponent.GameYear}年{gameTimeComponent.GameMonth:00}月{gameTimeComponent.GameDay:00}日");
        }

        /// <summary>
        /// 游戏内每分钟或每10分钟更新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="gameTimeComponent"></param>
        public static void RefreshTimeImage(this DlgMain self, GameTimeComponent gameTimeComponent)
        {
            int gameTimeImgShowCount = Mathf.FloorToInt(((float)gameTimeComponent.GameMinute) / 10);
            var gameTimeImages = self.View.EG_TimeRectTransform.childCount;

            for (int i = 0; i < gameTimeImages; i++)
            {
                self.View.EG_TimeRectTransform.GetChild(i).SetVisible(i <= gameTimeImgShowCount);
            }
        }

        /// <summary>
        /// 游戏内每季度更新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="gameTimeComponent"></param>
        public static void RefreshSeasonImage(this DlgMain self, GameTimeComponent gameTimeComponent)
        {
            self.View.E_SeasonImage.sprite = IconHelper.LoadIconSprite($"ui_time_{gameTimeComponent.Season.ToString()}");
        }

        /// <summary>
        /// 游戏内每小时更新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="gameTimeComponent"></param>
        public static void RefreshSunRiseImage(this DlgMain self, GameTimeComponent gameTimeComponent)
        {
            var targetRotate = new Vector3(0, 0, gameTimeComponent.GameHour * 15 - 90);
            self.View.EG_SunRiseRectTransform.DORotate(targetRotate, 1f);
        }
    }
}
