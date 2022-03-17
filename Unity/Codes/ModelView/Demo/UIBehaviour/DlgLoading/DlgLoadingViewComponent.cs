
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgLoadingViewComponent : Entity,IAwake,IDestroy
	{
		public UnityEngine.UI.Image E_LoadingImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoadingImage == null )
     			{
		    		this.m_E_LoadingImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Loading");
     			}
     			return this.m_E_LoadingImage;
     		}
     	}

		public UnityEngine.UI.Text ELabel_LoadingText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_LoadingText == null )
     			{
		    		this.m_ELabel_LoadingText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Loading/ELabel_Loading");
     			}
     			return this.m_ELabel_LoadingText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_LoadingImage = null;
			this.m_ELabel_LoadingText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_LoadingImage = null;
		private UnityEngine.UI.Text m_ELabel_LoadingText = null;
		public Transform uiTransform = null;
	}
}
