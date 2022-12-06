
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgLandLobbyViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_PromptText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PromptText == null )
     			{
		    		this.m_E_PromptText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"LandLobby/E_Prompt");
     			}
     			return this.m_E_PromptText;
     		}
     	}

		public UnityEngine.UI.Button E_EnterLandlordsButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EnterLandlordsButton == null )
     			{
		    		this.m_E_EnterLandlordsButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"LandLobby/E_EnterLandlords");
     			}
     			return this.m_E_EnterLandlordsButton;
     		}
     	}

		public UnityEngine.UI.Image E_EnterLandlordsImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EnterLandlordsImage == null )
     			{
		    		this.m_E_EnterLandlordsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"LandLobby/E_EnterLandlords");
     			}
     			return this.m_E_EnterLandlordsImage;
     		}
     	}

		public UnityEngine.UI.Text E_NickNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NickNameText == null )
     			{
		    		this.m_E_NickNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"LandLobby/E_NickName");
     			}
     			return this.m_E_NickNameText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_PromptText = null;
			this.m_E_EnterLandlordsButton = null;
			this.m_E_EnterLandlordsImage = null;
			this.m_E_NickNameText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_PromptText = null;
		private UnityEngine.UI.Button m_E_EnterLandlordsButton = null;
		private UnityEngine.UI.Image m_E_EnterLandlordsImage = null;
		private UnityEngine.UI.Text m_E_NickNameText = null;
		public Transform uiTransform = null;
	}
}
