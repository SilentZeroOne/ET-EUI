
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgInventoryViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.LoopVerticalScrollRect E_SlotsLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SlotsLoopVerticalScrollRect == null )
     			{
		    		this.m_E_SlotsLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"BG/SlotHolder/E_Slots");
     			}
     			return this.m_E_SlotsLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Image E_HeadIconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeadIconImage == null )
     			{
		    		this.m_E_HeadIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BG/HeadIconBg/E_HeadIcon");
     			}
     			return this.m_E_HeadIconImage;
     		}
     	}

		public UnityEngine.UI.Image E_GoldIconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GoldIconImage == null )
     			{
		    		this.m_E_GoldIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BG/GoldIconBg/E_GoldIcon");
     			}
     			return this.m_E_GoldIconImage;
     		}
     	}

		public TMPro.TextMeshProUGUI E_CoinCountTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CoinCountTextMeshProUGUI == null )
     			{
		    		this.m_E_CoinCountTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"BG/GoldIconBg/CoinCountBg/E_CoinCount");
     			}
     			return this.m_E_CoinCountTextMeshProUGUI;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_SlotsLoopVerticalScrollRect = null;
			this.m_E_HeadIconImage = null;
			this.m_E_GoldIconImage = null;
			this.m_E_CoinCountTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.LoopVerticalScrollRect m_E_SlotsLoopVerticalScrollRect = null;
		private UnityEngine.UI.Image m_E_HeadIconImage = null;
		private UnityEngine.UI.Image m_E_GoldIconImage = null;
		private TMPro.TextMeshProUGUI m_E_CoinCountTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
