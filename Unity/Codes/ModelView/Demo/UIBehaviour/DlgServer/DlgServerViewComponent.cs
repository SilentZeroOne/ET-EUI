
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class DlgServerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_BackGroundImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackGroundImage == null )
     			{
		    		this.m_E_BackGroundImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BackGround");
     			}
     			return this.m_E_BackGroundImage;
     		}
     	}

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
		    		this.m_EButton_EnterServerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_BackGround/EButton_EnterServer");
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
		    		this.m_EButton_EnterServerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BackGround/EButton_EnterServer");
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
		    		this.m_ELoopScrollList_ServersLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"E_BackGround/ELoopScrollList_Servers");
     			}
     			return this.m_ELoopScrollList_ServersLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackGroundImage = null;
			this.m_EButton_EnterServerButton = null;
			this.m_EButton_EnterServerImage = null;
			this.m_ELoopScrollList_ServersLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_BackGroundImage = null;
		private UnityEngine.UI.Button m_EButton_EnterServerButton = null;
		private UnityEngine.UI.Image m_EButton_EnterServerImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_ServersLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
