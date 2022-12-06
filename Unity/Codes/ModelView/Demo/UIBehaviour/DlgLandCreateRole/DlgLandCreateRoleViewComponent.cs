
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgLandCreateRoleViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.InputField E_NickNameInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NickNameInputField == null )
     			{
		    		this.m_E_NickNameInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"CreateRole/E_NickName");
     			}
     			return this.m_E_NickNameInputField;
     		}
     	}

		public UnityEngine.UI.Image E_NickNameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NickNameImage == null )
     			{
		    		this.m_E_NickNameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"CreateRole/E_NickName");
     			}
     			return this.m_E_NickNameImage;
     		}
     	}

		public UnityEngine.UI.Button E_ConfirmButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ConfirmButton == null )
     			{
		    		this.m_E_ConfirmButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"CreateRole/E_Confirm");
     			}
     			return this.m_E_ConfirmButton;
     		}
     	}

		public UnityEngine.UI.Image E_ConfirmImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ConfirmImage == null )
     			{
		    		this.m_E_ConfirmImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"CreateRole/E_Confirm");
     			}
     			return this.m_E_ConfirmImage;
     		}
     	}

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
		    		this.m_E_PromptText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"CreateRole/E_Prompt");
     			}
     			return this.m_E_PromptText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_NickNameInputField = null;
			this.m_E_NickNameImage = null;
			this.m_E_ConfirmButton = null;
			this.m_E_ConfirmImage = null;
			this.m_E_PromptText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.InputField m_E_NickNameInputField = null;
		private UnityEngine.UI.Image m_E_NickNameImage = null;
		private UnityEngine.UI.Button m_E_ConfirmButton = null;
		private UnityEngine.UI.Image m_E_ConfirmImage = null;
		private UnityEngine.UI.Text m_E_PromptText = null;
		public Transform uiTransform = null;
	}
}
