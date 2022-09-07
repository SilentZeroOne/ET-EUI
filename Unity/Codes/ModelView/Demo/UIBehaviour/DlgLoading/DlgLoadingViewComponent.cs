
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgLoadingViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_BackgroundImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackgroundImage == null )
     			{
		    		this.m_E_BackgroundImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Background");
     			}
     			return this.m_E_BackgroundImage;
     		}
     	}

		public UnityEngine.CanvasGroup E_BackgroundCanvasGroup
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackgroundCanvasGroup == null )
     			{
		    		this.m_E_BackgroundCanvasGroup = UIFindHelper.FindDeepChild<UnityEngine.CanvasGroup>(this.uiTransform.gameObject,"E_Background");
     			}
     			return this.m_E_BackgroundCanvasGroup;
     		}
     	}

		public TMPro.TextMeshProUGUI E_LoadingTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoadingTextMeshProUGUI == null )
     			{
		    		this.m_E_LoadingTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_Background/E_Loading");
     			}
     			return this.m_E_LoadingTextMeshProUGUI;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackgroundImage = null;
			this.m_E_BackgroundCanvasGroup = null;
			this.m_E_LoadingTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_BackgroundImage = null;
		private UnityEngine.CanvasGroup m_E_BackgroundCanvasGroup = null;
		private TMPro.TextMeshProUGUI m_E_LoadingTextMeshProUGUI = null;
		public Transform uiTransform = null;
		public RectTransform uiRectTransform = null;
	}
}
