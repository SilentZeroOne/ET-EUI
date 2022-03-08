using UnityEngine;

namespace ET
{
    public static class DlgServerSystem
    {
        public static void RegisterUIEvent(this DlgServer self)
        {
            self.View.EButton_EnterServerButton.AddListenerAsync(() => { return self.OnConfirmClickHandler(); });
        }

        public static void ShowWindow(this DlgServer self,Entity context = null)
        {
            int count = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfoList.Count;
        }

        public static void OnScrollItemRefreshHandler(this DlgServer self, Transform transform, int index)
        {
            
        }

        public static async ETTask OnConfirmClickHandler(this DlgServer self)
        {
            await ETTask.CompletedTask;
        }
    }
}