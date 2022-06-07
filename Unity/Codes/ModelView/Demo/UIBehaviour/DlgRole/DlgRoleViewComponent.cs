
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	[ComponentOf(typeof(UIBaseWindow))]
	public  class DlgRoleViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button EButton_DeleteRoleButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_DeleteRoleButton == null )
     			{
		    		this.m_EButton_DeleteRoleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_DeleteRole");
     			}
     			return this.m_EButton_DeleteRoleButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_DeleteRoleImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_DeleteRoleImage == null )
     			{
		    		this.m_EButton_DeleteRoleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_DeleteRole");
     			}
     			return this.m_EButton_DeleteRoleImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_CreateRoleButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CreateRoleButton == null )
     			{
		    		this.m_EButton_CreateRoleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_CreateRole");
     			}
     			return this.m_EButton_CreateRoleButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_CreateRoleImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CreateRoleImage == null )
     			{
		    		this.m_EButton_CreateRoleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_CreateRole");
     			}
     			return this.m_EButton_CreateRoleImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_EnterGameButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_EnterGameButton == null )
     			{
		    		this.m_EButton_EnterGameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_EnterGame");
     			}
     			return this.m_EButton_EnterGameButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_EnterGameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_EnterGameImage == null )
     			{
		    		this.m_EButton_EnterGameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_EnterGame");
     			}
     			return this.m_EButton_EnterGameImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_RolesListLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_RolesListLoopHorizontalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_RolesListLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"ELoopScrollList_RolesList");
     			}
     			return this.m_ELoopScrollList_RolesListLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.InputField EInputField_NameInputInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EInputField_NameInputInputField == null )
     			{
		    		this.m_EInputField_NameInputInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"EInputField_NameInput");
     			}
     			return this.m_EInputField_NameInputInputField;
     		}
     	}

		public UnityEngine.UI.Image EInputField_NameInputImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EInputField_NameInputImage == null )
     			{
		    		this.m_EInputField_NameInputImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EInputField_NameInput");
     			}
     			return this.m_EInputField_NameInputImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_DeleteRoleButton = null;
			this.m_EButton_DeleteRoleImage = null;
			this.m_EButton_CreateRoleButton = null;
			this.m_EButton_CreateRoleImage = null;
			this.m_EButton_EnterGameButton = null;
			this.m_EButton_EnterGameImage = null;
			this.m_ELoopScrollList_RolesListLoopHorizontalScrollRect = null;
			this.m_EInputField_NameInputInputField = null;
			this.m_EInputField_NameInputImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButton_DeleteRoleButton = null;
		private UnityEngine.UI.Image m_EButton_DeleteRoleImage = null;
		private UnityEngine.UI.Button m_EButton_CreateRoleButton = null;
		private UnityEngine.UI.Image m_EButton_CreateRoleImage = null;
		private UnityEngine.UI.Button m_EButton_EnterGameButton = null;
		private UnityEngine.UI.Image m_EButton_EnterGameImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_RolesListLoopHorizontalScrollRect = null;
		private UnityEngine.UI.InputField m_EInputField_NameInputInputField = null;
		private UnityEngine.UI.Image m_EInputField_NameInputImage = null;
		public Transform uiTransform = null;
	}
}
