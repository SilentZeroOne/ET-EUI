
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgServerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button EButton_EnterServerButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_EnterServerButton == null )
     			{
		    		this.m_EButton_EnterServerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BackGround/EButton_EnterServer");
     			}
     			return this.m_EButton_EnterServerButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_EnterServerImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_EnterServerImage == null )
     			{
		    		this.m_EButton_EnterServerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BackGround/EButton_EnterServer");
     			}
     			return this.m_EButton_EnterServerImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_ServersLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_ServersLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_ServersLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"BackGround/ELoopScrollList_Servers");
     			}
     			return this.m_ELoopScrollList_ServersLoopVerticalScrollRect;
     		}
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
     			if( this.m_EButton_ServerButton == null )
     			{
		    		this.m_EButton_ServerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BackGround/ELoopScrollList_Servers/Content/Item_Server/EButton_Server");
     			}
     			return this.m_EButton_ServerButton;
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
     			if( this.m_EButton_ServerImage == null )
     			{
		    		this.m_EButton_ServerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BackGround/ELoopScrollList_Servers/Content/Item_Server/EButton_Server");
     			}
     			return this.m_EButton_ServerImage;
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
     			if( this.m_EText_ServerNameText == null )
     			{
		    		this.m_EText_ServerNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"BackGround/ELoopScrollList_Servers/Content/Item_Server/EButton_Server/EText_ServerName");
     			}
     			return this.m_EText_ServerNameText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_EnterServerButton = null;
			this.m_EButton_EnterServerImage = null;
			this.m_ELoopScrollList_ServersLoopVerticalScrollRect = null;
			this.m_EButton_ServerButton = null;
			this.m_EButton_ServerImage = null;
			this.m_EText_ServerNameText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButton_EnterServerButton = null;
		private UnityEngine.UI.Image m_EButton_EnterServerImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_ServersLoopVerticalScrollRect = null;
		private UnityEngine.UI.Button m_EButton_ServerButton = null;
		private UnityEngine.UI.Image m_EButton_ServerImage = null;
		private UnityEngine.UI.Text m_EText_ServerNameText = null;
		public Transform uiTransform = null;
	}
}
