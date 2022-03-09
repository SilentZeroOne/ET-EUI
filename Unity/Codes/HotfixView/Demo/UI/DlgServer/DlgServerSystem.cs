using System;
using UnityEngine;

namespace ET
{
    public static class DlgServerSystem
    {
        public static void RegisterUIEvent(this DlgServer self)
        {
            self.View.EButton_EnterServerButton.AddListenerAsync(() => { return self.OnConfirmClickHandler(); });
            self.View.ELoopScrollList_ServersLoopVerticalScrollRect.AddItemRefreshListener((transform, i) =>
            {
                self.OnScrollItemRefreshHandler(transform, i);
            });
        }

        public static void ShowWindow(this DlgServer self,Entity context = null)
        {
            int count = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfoList.Count;
            self.AddUIScrollItems(ref self.ScrollItemServers, count);
            self.View.ELoopScrollList_ServersLoopVerticalScrollRect.SetVisible(true, count);
        }

        public static void HideWindow(this DlgServer self)
        {
            self.RemoveUIScrollItems(ref self.ScrollItemServers);
        }

        public static void OnScrollItemRefreshHandler(this DlgServer self, Transform transform, int index)
        {
            Scroll_Item_Server itemServer = self.ScrollItemServers[index].BindTrans(transform);
            var serverInfo = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfoList[index];
            itemServer.EButton_ServerImage.color =
                    self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId == serverInfo.Id? Color.red : Color.cyan;
            itemServer.EText_ServerNameText.SetText(serverInfo.ServerName);
            itemServer.EButton_ServerButton.AddListener(() => { self.OnSelectServerItemHandler(serverInfo.Id); });
        }

        public static void OnSelectServerItemHandler(this DlgServer self, long serverId)
        {
            self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId = int.Parse(serverId.ToString());
            Log.Debug($"当前选择的服务器为{serverId}");
            self.View.ELoopScrollList_ServersLoopVerticalScrollRect.RefillCells();
        }

        public static async ETTask OnConfirmClickHandler(this DlgServer self)
        {
            bool isSelect = self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId != 0;
            if (!isSelect)
            {
                Log.Error("请先选择区服！");//TODO 制作UI界面
                return;
            }

            try
            {
                int errorCode = await LoginHelper.GetRoles(self.ZoneScene());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                
                self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Role);
                self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Server);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}