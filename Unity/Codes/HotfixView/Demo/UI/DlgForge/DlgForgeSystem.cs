using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[Timer(TimerType.MakeQueueUI)]
	public class MakeQueueUITimer: ATimer<DlgForge>
	{
		public override void Run(DlgForge self)
		{
			self?.RefreshMakeQueue();
		}
	}
	
	[FriendClass(typeof(DlgForge))]
	[FriendClass(typeof(Scroll_Item_Production))]
	public static  class DlgForgeSystem
	{

		public static void RegisterUIEvent(this DlgForge self)
		{
			self.RegisterCloseEvent<DlgForge>(self.View.E_CloseButton);
			self.View.E_ProductionLoopVerticalScrollRect.AddItemRefreshListener(self.OnProductionRefreshHandler);
		}

		public static void ShowWindow(this DlgForge self, Entity contextData = null)
		{
			self.Refresh();
		}

		public static void HideWindow(this DlgForge self)
		{
			TimerComponent.Instance.Remove(ref self.MakeQueueTimer);
			self.RemoveUIScrollItems(ref self.ScrollItemProductions);
		}

		public static void Refresh(this DlgForge self)
		{
			self.RefreshMakeQueue();
			self.RefreshProduction();
			self.RefreshMaterialCount();
		}

		public static void RefreshMakeQueue(this DlgForge self)
		{
			Production production = self.ZoneScene().GetComponent<ForgeComponent>().GetProductionByIndex(0);
			self.View.ES_MakeQueue1.Refresh(production);
			
			production = self.ZoneScene().GetComponent<ForgeComponent>().GetProductionByIndex(1);
			self.View.ES_MakeQueue2.Refresh(production);

			TimerComponent.Instance.Remove(ref self.MakeQueueTimer);
			int count = self.ZoneScene().GetComponent<ForgeComponent>().GetMakingProductionQueueCount();
			if (count > 0)
			{
				TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 1000, TimerType.MakeQueueUI, self);
			}
		}

		public static void RefreshMaterialCount(this DlgForge self)
		{
			var numericComponent = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene());
			self.View.E_IronStoneCountText.SetText(numericComponent.GetAsInt(NumericType.IronCount).ToString());
			self.View.E_FurCountText.SetText(numericComponent.GetAsInt(NumericType.FurCount).ToString());
		}

		public static void RefreshProduction(this DlgForge self)
		{
			int unitLevel = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene()).GetAsInt(NumericType.Level);
			int count = ForgeProductionConfigCategory.Instance.GetProductionConfigCount(unitLevel);
			self.AddUIScrollItems(ref self.ScrollItemProductions, count);
			self.View.E_ProductionLoopVerticalScrollRect.SetVisible(true, count);
		}

		public static void OnProductionRefreshHandler(this DlgForge self, Transform transform, int index)
		{
			Scroll_Item_Production production = self.ScrollItemProductions[index].BindTrans(transform);
			var numericComponent = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene());
			int unitLevel = numericComponent.GetAsInt(NumericType.Level);
			ForgeProductionConfig config = ForgeProductionConfigCategory.Instance.GetProductionConfigByLevelIndex(unitLevel, index);

			production.ES_EquipItem.RefreshShowItem(config.ItemConfigId);
			
			StringBuilder sb = new StringBuilder();
			List<int> materialCounts = new List<int>();
			for (int i = 0; i < config.ConsumIds.Length; i++)
			{
				sb.Append(PlayerNumericConfigCategory.Instance.Get(config.ConsumIds[i]).Name);
				if (i != config.ConsumIds.Length)
				{
					sb.Append(",");
				}
				else
				{
					sb.Append(":");
				}

				materialCounts.Add(numericComponent.GetAsInt(config.ConsumIds[i]));
			}

			production.E_ConsumeTypeText.text = sb.ToString();
			
			sb.Clear();

			bool enableToMake = true;
			for (int i = 0; i < config.ConsumCounts.Length; i++)
			{
				sb.Append(config.ConsumCounts[i]);
				if (i != config.ConsumIds.Length)
				{
					sb.Append(",");
				}

				enableToMake &= materialCounts[i] > config.ConsumCounts[i];
			}
			production.E_ConsumeCountText.SetText(sb.ToString());
			production.E_ItemNameText.SetText(ItemConfigCategory.Instance.Get(config.ItemConfigId).Name);
			production.E_MakeButton.interactable = enableToMake;
			production.E_MakeButton.AddListenerAsync(() => { return self.OnStartProductionClickHandler(config.Id); });
		}

		public static async ETTask OnStartProductionClickHandler(this DlgForge self, int productionConfigId)
		{
			try
			{
				int errorCode = await ForgeHelper.StartProduction(self.ZoneScene(), productionConfigId);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				
				self.Refresh();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
		
	}
}
