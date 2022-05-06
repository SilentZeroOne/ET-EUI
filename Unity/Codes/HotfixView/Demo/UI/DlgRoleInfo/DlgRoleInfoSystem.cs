using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgRoleInfoSystem
	{

		public static void RegisterUIEvent(this DlgRoleInfo self)
		{
			self.RegisterCloseEvent<DlgRoleInfo>(self.View.EButton_CloseButton);
			self.View.ESAttributeItem_Agility.RegisterEvent(NumericType.Agility);
			self.View.ESAttributeItem_Spirit.RegisterEvent(NumericType.Spirit);
			self.View.ESAttributeItem_Stamina.RegisterEvent(NumericType.Stamina);
			self.View.ESAttributeItem_Strength.RegisterEvent(NumericType.Strength);
			self.View.E_AttributeStatusLoopVerticalScrollRect.AddItemRefreshListener(self.OnAttributeItemRefreshHandler);
		}

		public static void ShowWindow(this DlgRoleInfo self, Entity contextData = null)
		{
			self.Refresh();
		}

		public static void Refresh(this DlgRoleInfo self)
		{
			self.View.ESAttributeItem_Strength.Refresh(NumericType.Strength);
			self.View.ESAttributeItem_Stamina.Refresh(NumericType.Stamina);
			self.View.ESAttributeItem_Agility.Refresh(NumericType.Agility);
			self.View.ESAttributeItem_Spirit.Refresh(NumericType.Spirit);

			NumericComponent numericComponent = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene());
			self.View.E_CompatPowerText.text = numericComponent.GetAsLong(NumericType.CombatEffectiveness).ToString();
			self.View.E_AvailablePointText.text = numericComponent.GetAsLong(NumericType.AttributePoint).ToString();

			int count = PlayerNumericConfigCategory.Instance.GetShowConfigCount();
			self.AddUIScrollItems(ref self.ScrollItemStatus, count);
			self.View.E_AttributeStatusLoopVerticalScrollRect.SetVisible(true, count);
		}

		public static void OnAttributeItemRefreshHandler(this DlgRoleInfo self, Transform transform, int index)
		{
			Scroll_Item_Status scrollItemStatus = self.ScrollItemStatus[index].BindTrans(transform);
			PlayerNumericConfig config = PlayerNumericConfigCategory.Instance.GetConfigByIndex(index);
			scrollItemStatus.E_TitleText.text = config.Name + ":";
			scrollItemStatus.E_DamageText.text =
					UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene()).GetAsLong(config.Id).ToString();
		}

	}
}
