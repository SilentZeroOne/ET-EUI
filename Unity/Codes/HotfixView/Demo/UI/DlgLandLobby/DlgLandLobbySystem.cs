using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(DlgLandLobby))]
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    public static class DlgLandLobbySystem
    {

        public static void RegisterUIEvent(this DlgLandLobby self)
        {
            self.View.E_EnterLandlordsButton.AddListenerAsync(self.OnEnterGameBtnOnClickHandler);
        }

        public static void ShowWindow(this DlgLandLobby self, Entity contextData = null)
        {
            var roleInfo = self.ZoneScene().GetComponent<RoleInfoComponent>().RoleInfo;
            if (roleInfo != null || !roleInfo.IsDisposed)
            {
                self.View.E_NickNameText.SetText(roleInfo.NickName);
            }
        }

        public static async ETTask OnEnterGameBtnOnClickHandler(this DlgLandLobby self)
        {
            if (self.IsEntering)
            {
                return;
            }

            self.IsEntering = true;

            try
            {
                int errorCode = await LoginHelper.GetRealm(self.ZoneScene());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    self.IsEntering = false;
                    return;
                }

                errorCode = await LoginHelper.EnterGame(self.ZoneScene());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    self.IsEntering = false;
                    return;
                }

                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_LandLobby);
                //TODO 显示下一个窗口
            }
            catch (Exception e)
            {
                self.IsEntering = false;
                Log.Error(e.ToString());
            }
        }

    }
}
