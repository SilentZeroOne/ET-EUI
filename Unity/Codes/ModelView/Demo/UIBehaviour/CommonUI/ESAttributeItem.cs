
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class ESAttributeItem : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy 
	{
		public UnityEngine.UI.Text E_TitleText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TitleText == null )
     			{
		    		this.m_E_TitleText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Title");
     			}
     			return this.m_E_TitleText;
     		}
     	}

		public UnityEngine.UI.Text E_ValueText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ValueText == null )
     			{
		    		this.m_E_ValueText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Value");
     			}
     			return this.m_E_ValueText;
     		}
     	}

		public UnityEngine.UI.Button E_AddButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddButton == null )
     			{
		    		this.m_E_AddButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Add");
     			}
     			return this.m_E_AddButton;
     		}
     	}

		public UnityEngine.UI.Image E_AddImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddImage == null )
     			{
		    		this.m_E_AddImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Add");
     			}
     			return this.m_E_AddImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_TitleText = null;
			this.m_E_ValueText = null;
			this.m_E_AddButton = null;
			this.m_E_AddImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_TitleText = null;
		private UnityEngine.UI.Text m_E_ValueText = null;
		private UnityEngine.UI.Button m_E_AddButton = null;
		private UnityEngine.UI.Image m_E_AddImage = null;
		public Transform uiTransform = null;
	}
}
