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
            }
            else
            {
                self.SetCursor(self.DefaultCursor);
            }
		}
	}

    [FriendClass(typeof(DlgCursor))]
    [FriendClassAttribute(typeof(ET.DlgCursorViewComponent))]
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

        public static void SetCursorImage(this DlgCursor self, int itemType = -1)
        {
            CursorConfig config = itemType == -1 ? CursorConfigCategory.Instance.GetDefaultCursor() : CursorConfigCategory.Instance.GetCursorConfigByItemType(itemType);

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
                self.View.E_CursorImage.color = Color.white;
            }
        }

        public static void SyncPosition(this DlgCursor self)
        {
            var pos = InputHelper.GetMousePosition();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(self.View.uiRectTransform, pos,
                GlobalComponent.Instance.UICamera, out pos);

            self.View.E_CursorImage.transform.localPosition = pos;
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
