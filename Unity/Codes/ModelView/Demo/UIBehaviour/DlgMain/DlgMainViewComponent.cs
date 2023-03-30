
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgMainViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_StartGameButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartGameButton == null )
     			{
		    		this.m_E_StartGameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomParent/E_StartGame");
     			}
     			return this.m_E_StartGameButton;
     		}
     	}

		public UnityEngine.UI.Image E_StartGameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartGameImage == null )
     			{
		    		this.m_E_StartGameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomParent/E_StartGame");
     			}
     			return this.m_E_StartGameImage;
     		}
     	}

		public UnityEngine.RectTransform EG_SelfStandByRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_SelfStandByRectTransform == null )
     			{
		    		this.m_EG_SelfStandByRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"BottomParent/EG_SelfStandBy");
     			}
     			return this.m_EG_SelfStandByRectTransform;
     		}
     	}

		public UnityEngine.UI.Button E_UnReadyButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UnReadyButton == null )
     			{
		    		this.m_E_UnReadyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomParent/E_UnReady");
     			}
     			return this.m_E_UnReadyButton;
     		}
     	}

		public UnityEngine.UI.Image E_UnReadyImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UnReadyImage == null )
     			{
		    		this.m_E_UnReadyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomParent/E_UnReady");
     			}
     			return this.m_E_UnReadyImage;
     		}
     	}

		public UnityEngine.RectTransform EG_Player1StandByRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_Player1StandByRectTransform == null )
     			{
		    		this.m_EG_Player1StandByRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"MiddleParent/EG_Player1StandBy");
     			}
     			return this.m_EG_Player1StandByRectTransform;
     		}
     	}

		public UnityEngine.RectTransform EG_Player2StandByRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_Player2StandByRectTransform == null )
     			{
		    		this.m_EG_Player2StandByRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"MiddleParent/EG_Player2StandBy");
     			}
     			return this.m_EG_Player2StandByRectTransform;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_StartGameButton = null;
			this.m_E_StartGameImage = null;
			this.m_EG_SelfStandByRectTransform = null;
			this.m_E_UnReadyButton = null;
			this.m_E_UnReadyImage = null;
			this.m_EG_Player1StandByRectTransform = null;
			this.m_EG_Player2StandByRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_StartGameButton = null;
		private UnityEngine.UI.Image m_E_StartGameImage = null;
		private UnityEngine.RectTransform m_EG_SelfStandByRectTransform = null;
		private UnityEngine.UI.Button m_E_UnReadyButton = null;
		private UnityEngine.UI.Image m_E_UnReadyImage = null;
		private UnityEngine.RectTransform m_EG_Player1StandByRectTransform = null;
		private UnityEngine.RectTransform m_EG_Player2StandByRectTransform = null;
		public Transform uiTransform = null;
	}
}
