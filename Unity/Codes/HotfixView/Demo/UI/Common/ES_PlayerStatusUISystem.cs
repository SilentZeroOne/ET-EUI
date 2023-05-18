using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(ES_PlayerStatusUI))]
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    public static class ES_PlayerStatusUISystem
    {
        public static void SetStatus(this ES_PlayerStatusUI self, Unit unit)
        {
            var roleInfo = UnitHelper.GetCachedRoleInfo(self.ZoneScene(), unit.Id);
            self.E_NickNameTextMeshProUGUI.text = roleInfo.NickName;
            self.E_HuanLeDouTextMeshProUGUI.text = unit.GetComponent<NumericComponent>().GetAsInt(NumericType.HuanleBean).ToString();
        }

        public static void SetVisiable(this ES_PlayerStatusUI self, bool visiable)
        {
            self.uiTransform.SetVisible(visiable);
            if (visiable)
            {
                LayoutRebuilder.MarkLayoutForRebuild(self.uiTransform.GetComponent<RectTransform>());
            }
        }
    }
}