
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgItemTooltipViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.RectTransform EG_ItemTooltipRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_ItemTooltipRectTransform == null )
     			{
		    		this.m_EG_ItemTooltipRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_ItemTooltip");
     			}
     			return this.m_EG_ItemTooltipRectTransform;
     		}
     	}

		public TMPro.TextMeshProUGUI E_NameTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NameTextMeshProUGUI == null )
     			{
		    		this.m_E_NameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_ItemTooltip/Top/E_Name");
     			}
     			return this.m_E_NameTextMeshProUGUI;
     		}
     	}

		public TMPro.TextMeshProUGUI E_TypeTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TypeTextMeshProUGUI == null )
     			{
		    		this.m_E_TypeTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_ItemTooltip/Top/E_Type");
     			}
     			return this.m_E_TypeTextMeshProUGUI;
     		}
     	}

		public TMPro.TextMeshProUGUI E_DescriptionTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DescriptionTextMeshProUGUI == null )
     			{
		    		this.m_E_DescriptionTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_ItemTooltip/Middle/E_Description");
     			}
     			return this.m_E_DescriptionTextMeshProUGUI;
     		}
     	}

		public UnityEngine.RectTransform EG_SpaceRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_SpaceRectTransform == null )
     			{
		    		this.m_EG_SpaceRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_ItemTooltip/EG_Space");
     			}
     			return this.m_EG_SpaceRectTransform;
     		}
     	}

		public UnityEngine.RectTransform EG_BottomRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_BottomRectTransform == null )
     			{
		    		this.m_EG_BottomRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_ItemTooltip/EG_Bottom");
     			}
     			return this.m_EG_BottomRectTransform;
     		}
     	}

		public UnityEngine.UI.Text E_CoinText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CoinText == null )
     			{
		    		this.m_E_CoinText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EG_ItemTooltip/EG_Bottom/CoinImage/E_Coin");
     			}
     			return this.m_E_CoinText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EG_ItemTooltipRectTransform = null;
			this.m_E_NameTextMeshProUGUI = null;
			this.m_E_TypeTextMeshProUGUI = null;
			this.m_E_DescriptionTextMeshProUGUI = null;
			this.m_EG_SpaceRectTransform = null;
			this.m_EG_BottomRectTransform = null;
			this.m_E_CoinText = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_ItemTooltipRectTransform = null;
		private TMPro.TextMeshProUGUI m_E_NameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_TypeTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_DescriptionTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_SpaceRectTransform = null;
		private UnityEngine.RectTransform m_EG_BottomRectTransform = null;
		private UnityEngine.UI.Text m_E_CoinText = null;
		public Transform uiTransform = null;
		public RectTransform uiRectTransform = null;
	}
}
