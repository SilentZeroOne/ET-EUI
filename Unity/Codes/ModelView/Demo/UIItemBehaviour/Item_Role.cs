
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_Item_Role : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Role BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Text E_NameText
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
     				if( this.m_E_NameText == null )
     				{
		    			this.m_E_NameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Name");
     				}
     				return this.m_E_NameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Name");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_CreateTimeText
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
     				if( this.m_E_CreateTimeText == null )
     				{
		    			this.m_E_CreateTimeText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_CreateTime");
     				}
     				return this.m_E_CreateTimeText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_CreateTime");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_NameText = null;
			this.m_E_CreateTimeText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_NameText = null;
		private UnityEngine.UI.Text m_E_CreateTimeText = null;
		public Transform uiTransform = null;
	}
}
