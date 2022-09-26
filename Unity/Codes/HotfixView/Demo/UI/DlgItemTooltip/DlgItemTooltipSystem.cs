using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(DlgItemTooltip))]
    [FriendClassAttribute(typeof(ET.DlgItemTooltipViewComponent))]
    public static class DlgItemTooltipSystem
    {

        public static void RegisterUIEvent(this DlgItemTooltip self)
        {

        }

        public static void ShowWindow(this DlgItemTooltip self, Entity contextData = null)
        {
            self.Refresh(contextData as Item).Coroutine();
            self.AdjustTransform();
        }

        public static async ETTask Refresh(this DlgItemTooltip self, Item item)
        {
            self.View.E_NameTextMeshProUGUI.SetText(item.Config.ItemName);
            self.View.E_TypeTextMeshProUGUI.SetText(((ItemType)item.Config.ItemType).GetName());
            self.View.E_DescriptionTextMeshProUGUI.SetText(item.Config.ItemDescription);
            self.View.EG_BottomRectTransform.SetVisible(item.Config.ItemPrice > 0);
            self.View.EG_SpaceRectTransform.SetVisible(!(item.Config.ItemPrice > 0));
            self.View.E_CoinText.SetText((item.Config.ItemPrice * ((float)item.Config.SellPercentage / 100)).ToString());

            await TimerComponent.Instance.WaitAsync(10);
            LayoutRebuilder.ForceRebuildLayoutImmediate(self.View.EG_ItemTooltipRectTransform);
        }

        public static void AdjustTransform(this DlgItemTooltip self)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(self.View.uiRectTransform, InputHelper.GetMousePosition(),
                GlobalComponent.Instance.UICamera, out Vector2 pos);
            self.View.EG_ItemTooltipRectTransform.localPosition = pos + new Vector2(1, 1);
        }
        
    }
}
