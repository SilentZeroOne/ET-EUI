
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class Scroll_Item_LevelDetail : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_LevelDetail BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Text E_LevelTitleText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_LevelTitleText == null )
     				{
		    			this.m_E_LevelTitleText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"DetailContent/E_LevelTitle");
     				}
     				return this.m_E_LevelTitleText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"DetailContent/E_LevelTitle");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_LevelEnterText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_LevelEnterText == null )
     				{
		    			this.m_E_LevelEnterText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"DetailContent/E_LevelEnter");
     				}
     				return this.m_E_LevelEnterText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"DetailContent/E_LevelEnter");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_EnterButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_EnterButton == null )
     				{
		    			this.m_E_EnterButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Enter");
     				}
     				return this.m_E_EnterButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Enter");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_EnterImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_EnterImage == null )
     				{
		    			this.m_E_EnterImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Enter");
     				}
     				return this.m_E_EnterImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Enter");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_CurrentStatusText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_CurrentStatusText == null )
     				{
		    			this.m_E_CurrentStatusText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_CurrentStatus");
     				}
     				return this.m_E_CurrentStatusText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_CurrentStatus");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_LevelTitleText = null;
			this.m_E_LevelEnterText = null;
			this.m_E_EnterButton = null;
			this.m_E_EnterImage = null;
			this.m_E_CurrentStatusText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_LevelTitleText = null;
		private UnityEngine.UI.Text m_E_LevelEnterText = null;
		private UnityEngine.UI.Button m_E_EnterButton = null;
		private UnityEngine.UI.Image m_E_EnterImage = null;
		private UnityEngine.UI.Text m_E_CurrentStatusText = null;
		public Transform uiTransform = null;
	}
}
