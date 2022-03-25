
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_Item_Server : Entity,IAwake,IDestroy,IUIScrollItem
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Server BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button EButton_ServerButton
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
     				if( this.m_EButton_ServerButton == null )
     				{
		    			this.m_EButton_ServerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_Server");
     				}
     				return this.m_EButton_ServerButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_Server");
     			}
     		}
     	}

		public UnityEngine.UI.Image EButton_ServerImage
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
     				if( this.m_EButton_ServerImage == null )
     				{
		    			this.m_EButton_ServerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_Server");
     				}
     				return this.m_EButton_ServerImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_Server");
     			}
     		}
     	}

		public UnityEngine.UI.Text EText_ServerNameText
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
     				if( this.m_EText_ServerNameText == null )
     				{
		    			this.m_EText_ServerNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EButton_Server/EText_ServerName");
     				}
     				return this.m_EText_ServerNameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EButton_Server/EText_ServerName");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_ServerButton = null;
			this.m_EButton_ServerImage = null;
			this.m_EText_ServerNameText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButton_ServerButton = null;
		private UnityEngine.UI.Image m_EButton_ServerImage = null;
		private UnityEngine.UI.Text m_EText_ServerNameText = null;
		public Transform uiTransform = null;
	}
}
