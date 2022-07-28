﻿
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgMainViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button EButton_RoleButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_RoleButton == null )
     			{
		    		this.m_EButton_RoleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Buttons/EButton_Role");
     			}
     			return this.m_EButton_RoleButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_RoleImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_RoleImage == null )
     			{
		    		this.m_EButton_RoleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Buttons/EButton_Role");
     			}
     			return this.m_EButton_RoleImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_BuildButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_BuildButton == null )
     			{
		    		this.m_EButton_BuildButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Buttons/EButton_Build");
     			}
     			return this.m_EButton_BuildButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_BuildImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_BuildImage == null )
     			{
		    		this.m_EButton_BuildImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Buttons/EButton_Build");
     			}
     			return this.m_EButton_BuildImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_AdvantureButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_AdvantureButton == null )
     			{
		    		this.m_EButton_AdvantureButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Buttons/EButton_Advanture");
     			}
     			return this.m_EButton_AdvantureButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_AdvantureImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_AdvantureImage == null )
     			{
		    		this.m_EButton_AdvantureImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Buttons/EButton_Advanture");
     			}
     			return this.m_EButton_AdvantureImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_QuestButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_QuestButton == null )
     			{
		    		this.m_EButton_QuestButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Buttons/EButton_Quest");
     			}
     			return this.m_EButton_QuestButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_QuestImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_QuestImage == null )
     			{
		    		this.m_EButton_QuestImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Buttons/EButton_Quest");
     			}
     			return this.m_EButton_QuestImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_BagButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_BagButton == null )
     			{
		    		this.m_EButton_BagButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Buttons/EButton_Bag");
     			}
     			return this.m_EButton_BagButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_BagImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_BagImage == null )
     			{
		    		this.m_EButton_BagImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Buttons/EButton_Bag");
     			}
     			return this.m_EButton_BagImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_RankButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_RankButton == null )
     			{
		    		this.m_EButton_RankButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Buttons/EButton_Rank");
     			}
     			return this.m_EButton_RankButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_RankImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_RankImage == null )
     			{
		    		this.m_EButton_RankImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Buttons/EButton_Rank");
     			}
     			return this.m_EButton_RankImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_ChatButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ChatButton == null )
     			{
		    		this.m_EButton_ChatButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Buttons/EButton_Chat");
     			}
     			return this.m_EButton_ChatButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_ChatImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ChatImage == null )
     			{
		    		this.m_EButton_ChatImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Buttons/EButton_Chat");
     			}
     			return this.m_EButton_ChatImage;
     		}
     	}

		public UnityEngine.UI.Image E_HeadImageRingImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeadImageRingImage == null )
     			{
		    		this.m_E_HeadImageRingImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Displays/E_HeadImageRing");
     			}
     			return this.m_E_HeadImageRingImage;
     		}
     	}

		public UnityEngine.UI.Image E_HeadImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeadImage == null )
     			{
		    		this.m_E_HeadImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Displays/E_HeadImageRing/E_Head");
     			}
     			return this.m_E_HeadImage;
     		}
     	}

		public UnityEngine.UI.Text E_LevelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LevelText == null )
     			{
		    		this.m_E_LevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Displays/E_HeadImageRing/E_Head/E_Level");
     			}
     			return this.m_E_LevelText;
     		}
     	}

		public UnityEngine.UI.Image E_GoldBgImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GoldBgImage == null )
     			{
		    		this.m_E_GoldBgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Displays/E_GoldBg");
     			}
     			return this.m_E_GoldBgImage;
     		}
     	}

		public UnityEngine.UI.Image E_GoldImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GoldImage == null )
     			{
		    		this.m_E_GoldImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Displays/E_GoldBg/E_Gold");
     			}
     			return this.m_E_GoldImage;
     		}
     	}

		public UnityEngine.UI.Text E_GoldText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GoldText == null )
     			{
		    		this.m_E_GoldText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Displays/E_GoldBg/E_Gold/E_Gold");
     			}
     			return this.m_E_GoldText;
     		}
     	}

		public UnityEngine.UI.Image E_ExperienceBgImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExperienceBgImage == null )
     			{
		    		this.m_E_ExperienceBgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Displays/E_ExperienceBg");
     			}
     			return this.m_E_ExperienceBgImage;
     		}
     	}

		public UnityEngine.UI.Image E_ExperienceImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExperienceImage == null )
     			{
		    		this.m_E_ExperienceImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Displays/E_ExperienceBg/E_Experience");
     			}
     			return this.m_E_ExperienceImage;
     		}
     	}

		public UnityEngine.UI.Text E_ExperienceText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExperienceText == null )
     			{
		    		this.m_E_ExperienceText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Displays/E_ExperienceBg/E_Experience/E_Experience");
     			}
     			return this.m_E_ExperienceText;
     		}
     	}

		public UnityEngine.UI.Text E_PingText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PingText == null )
     			{
		    		this.m_E_PingText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Displays/PingTitle/E_Ping");
     			}
     			return this.m_E_PingText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_RoleButton = null;
			this.m_EButton_RoleImage = null;
			this.m_EButton_BuildButton = null;
			this.m_EButton_BuildImage = null;
			this.m_EButton_AdvantureButton = null;
			this.m_EButton_AdvantureImage = null;
			this.m_EButton_QuestButton = null;
			this.m_EButton_QuestImage = null;
			this.m_EButton_BagButton = null;
			this.m_EButton_BagImage = null;
			this.m_EButton_RankButton = null;
			this.m_EButton_RankImage = null;
			this.m_EButton_ChatButton = null;
			this.m_EButton_ChatImage = null;
			this.m_E_HeadImageRingImage = null;
			this.m_E_HeadImage = null;
			this.m_E_LevelText = null;
			this.m_E_GoldBgImage = null;
			this.m_E_GoldImage = null;
			this.m_E_GoldText = null;
			this.m_E_ExperienceBgImage = null;
			this.m_E_ExperienceImage = null;
			this.m_E_ExperienceText = null;
			this.m_E_PingText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButton_RoleButton = null;
		private UnityEngine.UI.Image m_EButton_RoleImage = null;
		private UnityEngine.UI.Button m_EButton_BuildButton = null;
		private UnityEngine.UI.Image m_EButton_BuildImage = null;
		private UnityEngine.UI.Button m_EButton_AdvantureButton = null;
		private UnityEngine.UI.Image m_EButton_AdvantureImage = null;
		private UnityEngine.UI.Button m_EButton_QuestButton = null;
		private UnityEngine.UI.Image m_EButton_QuestImage = null;
		private UnityEngine.UI.Button m_EButton_BagButton = null;
		private UnityEngine.UI.Image m_EButton_BagImage = null;
		private UnityEngine.UI.Button m_EButton_RankButton = null;
		private UnityEngine.UI.Image m_EButton_RankImage = null;
		private UnityEngine.UI.Button m_EButton_ChatButton = null;
		private UnityEngine.UI.Image m_EButton_ChatImage = null;
		private UnityEngine.UI.Image m_E_HeadImageRingImage = null;
		private UnityEngine.UI.Image m_E_HeadImage = null;
		private UnityEngine.UI.Text m_E_LevelText = null;
		private UnityEngine.UI.Image m_E_GoldBgImage = null;
		private UnityEngine.UI.Image m_E_GoldImage = null;
		private UnityEngine.UI.Text m_E_GoldText = null;
		private UnityEngine.UI.Image m_E_ExperienceBgImage = null;
		private UnityEngine.UI.Image m_E_ExperienceImage = null;
		private UnityEngine.UI.Text m_E_ExperienceText = null;
		private UnityEngine.UI.Text m_E_PingText = null;
		public Transform uiTransform = null;
	}
}
