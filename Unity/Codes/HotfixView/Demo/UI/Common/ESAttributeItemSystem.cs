using System;

namespace ET
{
    [FriendClass(typeof(ESAttributeItem))]
    public static class ESAttributeItemSystem
    {
        public static void Refresh(this ESAttributeItem self, int numericType)
        {
            self.E_ValueText.text = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene()).GetComponent<NumericComponent>()
                    .GetAsLong(numericType).ToString();
        }
        
        public static void RegisterEvent(this ESAttributeItem self, int numericType)
        {
            self.E_AddButton.AddListenerAsync(() =>
            {
                return self.RequestAddAttribute(numericType);
            });
        }
        
        public static async ETTask RequestAddAttribute(this ESAttributeItem self, int numericType)
        {
            try
            {
                int errorCode = await NumericHelper.RequestAddAttributePoint(self.ZoneScene(), numericType);
                if (errorCode != ErrorCode.ERR_Success)
                {
                    return;
                }
                Log.Debug("加点成功！");
                self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgRoleInfo>()?.Refresh();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            await ETTask.CompletedTask;
        }
    }
}