
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

		public UnityEngine.RectTransform EG_StandByRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_StandByRectTransform == null )
     			{
		    		this.m_EG_StandByRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"BottomParent/EG_StandBy");
     			}
     			return this.m_EG_StandByRectTransform;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_StartGameButton = null;
			this.m_E_StartGameImage = null;
			this.m_EG_StandByRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_StartGameButton = null;
		private UnityEngine.UI.Image m_E_StartGameImage = null;
		private UnityEngine.RectTransform m_EG_StandByRectTransform = null;
		public Transform uiTransform = null;
	}
}
