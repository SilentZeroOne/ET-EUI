﻿
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgLandViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.InputField E_AccountInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AccountInputField == null )
     			{
		    		this.m_E_AccountInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"LandLogin/E_Account");
     			}
     			return this.m_E_AccountInputField;
     		}
     	}

		public UnityEngine.UI.Image E_AccountImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AccountImage == null )
     			{
		    		this.m_E_AccountImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"LandLogin/E_Account");
     			}
     			return this.m_E_AccountImage;
     		}
     	}

		public UnityEngine.UI.InputField E_PasswordInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PasswordInputField == null )
     			{
		    		this.m_E_PasswordInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"LandLogin/E_Password");
     			}
     			return this.m_E_PasswordInputField;
     		}
     	}

		public UnityEngine.UI.Image E_PasswordImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PasswordImage == null )
     			{
		    		this.m_E_PasswordImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"LandLogin/E_Password");
     			}
     			return this.m_E_PasswordImage;
     		}
     	}

		public UnityEngine.UI.Button E_LoginButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoginButton == null )
     			{
		    		this.m_E_LoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"LandLogin/E_Login");
     			}
     			return this.m_E_LoginButton;
     		}
     	}

		public UnityEngine.UI.Image E_LoginImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoginImage == null )
     			{
		    		this.m_E_LoginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"LandLogin/E_Login");
     			}
     			return this.m_E_LoginImage;
     		}
     	}

		public UnityEngine.UI.Button E_RegisterButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RegisterButton == null )
     			{
		    		this.m_E_RegisterButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"LandLogin/E_Register");
     			}
     			return this.m_E_RegisterButton;
     		}
     	}

		public UnityEngine.UI.Image E_RegisterImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RegisterImage == null )
     			{
		    		this.m_E_RegisterImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"LandLogin/E_Register");
     			}
     			return this.m_E_RegisterImage;
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
		    		this.m_E_PromptText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"LandLogin/E_Prompt");
     			}
     			return this.m_E_PromptText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_AccountInputField = null;
			this.m_E_AccountImage = null;
			this.m_E_PasswordInputField = null;
			this.m_E_PasswordImage = null;
			this.m_E_LoginButton = null;
			this.m_E_LoginImage = null;
			this.m_E_RegisterButton = null;
			this.m_E_RegisterImage = null;
			this.m_E_PromptText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.InputField m_E_AccountInputField = null;
		private UnityEngine.UI.Image m_E_AccountImage = null;
		private UnityEngine.UI.InputField m_E_PasswordInputField = null;
		private UnityEngine.UI.Image m_E_PasswordImage = null;
		private UnityEngine.UI.Button m_E_LoginButton = null;
		private UnityEngine.UI.Image m_E_LoginImage = null;
		private UnityEngine.UI.Button m_E_RegisterButton = null;
		private UnityEngine.UI.Image m_E_RegisterImage = null;
		private UnityEngine.UI.Text m_E_PromptText = null;
		public Transform uiTransform = null;
	}
}
