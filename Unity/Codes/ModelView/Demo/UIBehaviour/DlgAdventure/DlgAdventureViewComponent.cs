
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	[ComponentOf(typeof(UIBaseWindow))]
	public  class DlgAdventureViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.RectTransform EG_ContentRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_ContentRectTransform == null )
     			{
		    		this.m_EG_ContentRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_Content");
     			}
     			return this.m_EG_ContentRectTransform;
     		}
     	}

		public UnityEngine.UI.Button E_CloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseButton == null )
     			{
		    		this.m_E_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_Content/E_Close");
     			}
     			return this.m_E_CloseButton;
     		}
     	}

		public UnityEngine.UI.Image E_CloseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseImage == null )
     			{
		    		this.m_E_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_Content/E_Close");
     			}
     			return this.m_E_CloseImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_LevelDetailLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LevelDetailLoopVerticalScrollRect == null )
     			{
		    		this.m_E_LevelDetailLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"EG_Content/Background/E_LevelDetail");
     			}
     			return this.m_E_LevelDetailLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EG_ContentRectTransform = null;
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.m_E_LevelDetailLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_ContentRectTransform = null;
		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_LevelDetailLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
