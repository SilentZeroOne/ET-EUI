
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgMainViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_LeaveRoomButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LeaveRoomButton == null )
     			{
		    		this.m_E_LeaveRoomButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"TopParent/E_LeaveRoom");
     			}
     			return this.m_E_LeaveRoomButton;
     		}
     	}

		public UnityEngine.UI.Image E_LeaveRoomImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LeaveRoomImage == null )
     			{
		    		this.m_E_LeaveRoomImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"TopParent/E_LeaveRoom");
     			}
     			return this.m_E_LeaveRoomImage;
     		}
     	}

		public UnityEngine.RectTransform EG_LordCardBgRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_LordCardBgRectTransform == null )
     			{
		    		this.m_EG_LordCardBgRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"TopParent/EG_LordCardBg");
     			}
     			return this.m_EG_LordCardBgRectTransform;
     		}
     	}

		public UnityEngine.RectTransform EG_AfterStartGameButtonsRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_AfterStartGameButtonsRectTransform == null )
     			{
		    		this.m_EG_AfterStartGameButtonsRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"BottomParent/EG_AfterStartGameButtons");
     			}
     			return this.m_EG_AfterStartGameButtonsRectTransform;
     		}
     	}

		public UnityEngine.UI.Button E_RobButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RobButton == null )
     			{
		    		this.m_E_RobButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomParent/EG_AfterStartGameButtons/E_Rob");
     			}
     			return this.m_E_RobButton;
     		}
     	}

		public UnityEngine.UI.Image E_RobImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RobImage == null )
     			{
		    		this.m_E_RobImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomParent/EG_AfterStartGameButtons/E_Rob");
     			}
     			return this.m_E_RobImage;
     		}
     	}

		public UnityEngine.UI.Button E_NotRobButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NotRobButton == null )
     			{
		    		this.m_E_NotRobButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomParent/EG_AfterStartGameButtons/E_NotRob");
     			}
     			return this.m_E_NotRobButton;
     		}
     	}

		public UnityEngine.UI.Image E_NotRobImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NotRobImage == null )
     			{
		    		this.m_E_NotRobImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomParent/EG_AfterStartGameButtons/E_NotRob");
     			}
     			return this.m_E_NotRobImage;
     		}
     	}

		public UnityEngine.UI.Button E_PlayCardButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PlayCardButton == null )
     			{
		    		this.m_E_PlayCardButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomParent/EG_AfterStartGameButtons/E_PlayCard");
     			}
     			return this.m_E_PlayCardButton;
     		}
     	}

		public UnityEngine.UI.Image E_PlayCardImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PlayCardImage == null )
     			{
		    		this.m_E_PlayCardImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomParent/EG_AfterStartGameButtons/E_PlayCard");
     			}
     			return this.m_E_PlayCardImage;
     		}
     	}

		public UnityEngine.UI.Button E_PassButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PassButton == null )
     			{
		    		this.m_E_PassButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomParent/EG_AfterStartGameButtons/E_Pass");
     			}
     			return this.m_E_PassButton;
     		}
     	}

		public UnityEngine.UI.Image E_PassImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PassImage == null )
     			{
		    		this.m_E_PassImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomParent/EG_AfterStartGameButtons/E_Pass");
     			}
     			return this.m_E_PassImage;
     		}
     	}

		public UnityEngine.UI.Button E_StartGameButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartGameButton == null )
     			{
		    		this.m_E_StartGameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomParent/E_StartGame");
     			}
     			return this.m_E_StartGameButton;
     		}
     	}

		public UnityEngine.UI.Image E_StartGameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartGameImage == null )
     			{
		    		this.m_E_StartGameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomParent/E_StartGame");
     			}
     			return this.m_E_StartGameImage;
     		}
     	}

		public UnityEngine.RectTransform EG_SelfStandByRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_SelfStandByRectTransform == null )
     			{
		    		this.m_EG_SelfStandByRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"BottomParent/EG_SelfStandBy");
     			}
     			return this.m_EG_SelfStandByRectTransform;
     		}
     	}

		public UnityEngine.UI.Button E_UnReadyButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UnReadyButton == null )
     			{
		    		this.m_E_UnReadyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomParent/E_UnReady");
     			}
     			return this.m_E_UnReadyButton;
     		}
     	}

		public UnityEngine.UI.Image E_UnReadyImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UnReadyImage == null )
     			{
		    		this.m_E_UnReadyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomParent/E_UnReady");
     			}
     			return this.m_E_UnReadyImage;
     		}
     	}

		public UnityEngine.RectTransform EG_CardParentRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_CardParentRectTransform == null )
     			{
		    		this.m_EG_CardParentRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"BottomParent/EG_CardParent");
     			}
     			return this.m_EG_CardParentRectTransform;
     		}
     	}

		public UnityEngine.UI.Button E_CardTemplateButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CardTemplateButton == null )
     			{
		    		this.m_E_CardTemplateButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomParent/EG_CardParent/E_CardTemplate");
     			}
     			return this.m_E_CardTemplateButton;
     		}
     	}

		public UnityEngine.UI.Image E_CardTemplateImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CardTemplateImage == null )
     			{
		    		this.m_E_CardTemplateImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomParent/EG_CardParent/E_CardTemplate");
     			}
     			return this.m_E_CardTemplateImage;
     		}
     	}

		public UnityEngine.RectTransform EG_Player1StandByRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_Player1StandByRectTransform == null )
     			{
		    		this.m_EG_Player1StandByRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"MiddleParent/EG_Player1StandBy");
     			}
     			return this.m_EG_Player1StandByRectTransform;
     		}
     	}

		public UnityEngine.RectTransform EG_Player2StandByRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_Player2StandByRectTransform == null )
     			{
		    		this.m_EG_Player2StandByRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"MiddleParent/EG_Player2StandBy");
     			}
     			return this.m_EG_Player2StandByRectTransform;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_LeaveRoomButton = null;
			this.m_E_LeaveRoomImage = null;
			this.m_EG_LordCardBgRectTransform = null;
			this.m_EG_AfterStartGameButtonsRectTransform = null;
			this.m_E_RobButton = null;
			this.m_E_RobImage = null;
			this.m_E_NotRobButton = null;
			this.m_E_NotRobImage = null;
			this.m_E_PlayCardButton = null;
			this.m_E_PlayCardImage = null;
			this.m_E_PassButton = null;
			this.m_E_PassImage = null;
			this.m_E_StartGameButton = null;
			this.m_E_StartGameImage = null;
			this.m_EG_SelfStandByRectTransform = null;
			this.m_E_UnReadyButton = null;
			this.m_E_UnReadyImage = null;
			this.m_EG_CardParentRectTransform = null;
			this.m_E_CardTemplateButton = null;
			this.m_E_CardTemplateImage = null;
			this.m_EG_Player1StandByRectTransform = null;
			this.m_EG_Player2StandByRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_LeaveRoomButton = null;
		private UnityEngine.UI.Image m_E_LeaveRoomImage = null;
		private UnityEngine.RectTransform m_EG_LordCardBgRectTransform = null;
		private UnityEngine.RectTransform m_EG_AfterStartGameButtonsRectTransform = null;
		private UnityEngine.UI.Button m_E_RobButton = null;
		private UnityEngine.UI.Image m_E_RobImage = null;
		private UnityEngine.UI.Button m_E_NotRobButton = null;
		private UnityEngine.UI.Image m_E_NotRobImage = null;
		private UnityEngine.UI.Button m_E_PlayCardButton = null;
		private UnityEngine.UI.Image m_E_PlayCardImage = null;
		private UnityEngine.UI.Button m_E_PassButton = null;
		private UnityEngine.UI.Image m_E_PassImage = null;
		private UnityEngine.UI.Button m_E_StartGameButton = null;
		private UnityEngine.UI.Image m_E_StartGameImage = null;
		private UnityEngine.RectTransform m_EG_SelfStandByRectTransform = null;
		private UnityEngine.UI.Button m_E_UnReadyButton = null;
		private UnityEngine.UI.Image m_E_UnReadyImage = null;
		private UnityEngine.RectTransform m_EG_CardParentRectTransform = null;
		private UnityEngine.UI.Button m_E_CardTemplateButton = null;
		private UnityEngine.UI.Image m_E_CardTemplateImage = null;
		private UnityEngine.RectTransform m_EG_Player1StandByRectTransform = null;
		private UnityEngine.RectTransform m_EG_Player2StandByRectTransform = null;
		public Transform uiTransform = null;
	}
}
