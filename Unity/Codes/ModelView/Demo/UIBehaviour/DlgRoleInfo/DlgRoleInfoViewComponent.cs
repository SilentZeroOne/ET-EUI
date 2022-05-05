
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgRoleInfoViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button EButton_CloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CloseButton == null )
     			{
		    		this.m_EButton_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Background/EButton_Close");
     			}
     			return this.m_EButton_CloseButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_CloseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CloseImage == null )
     			{
		    		this.m_EButton_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Background/EButton_Close");
     			}
     			return this.m_EButton_CloseImage;
     		}
     	}

		public UnityEngine.UI.Image EquipmentBgImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EquipmentBgImage == null )
     			{
		    		this.m_EquipmentBgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Background/EquipmentBg");
     			}
     			return this.m_EquipmentBgImage;
     		}
     	}

		public UnityEngine.UI.Text E_CompatPowerText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CompatPowerText == null )
     			{
		    		this.m_E_CompatPowerText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/EquipmentBg/CompatPower/E_CompatPower");
     			}
     			return this.m_E_CompatPowerText;
     		}
     	}

		public UnityEngine.UI.Text E_AvailablePointText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AvailablePointText == null )
     			{
		    		this.m_E_AvailablePointText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/AddPointBg/CurrentPoint/E_AvailablePoint");
     			}
     			return this.m_E_AvailablePointText;
     		}
     	}

		public UnityEngine.UI.Text E_StrengthText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StrengthText == null )
     			{
		    		this.m_E_StrengthText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/AddPointBg/Strength/E_Strength");
     			}
     			return this.m_E_StrengthText;
     		}
     	}

		public UnityEngine.UI.Button E_AddStrengthButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddStrengthButton == null )
     			{
		    		this.m_E_AddStrengthButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Background/AddPointBg/Strength/E_AddStrength");
     			}
     			return this.m_E_AddStrengthButton;
     		}
     	}

		public UnityEngine.UI.Image E_AddStrengthImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddStrengthImage == null )
     			{
		    		this.m_E_AddStrengthImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Background/AddPointBg/Strength/E_AddStrength");
     			}
     			return this.m_E_AddStrengthImage;
     		}
     	}

		public UnityEngine.UI.Text E_StaminaText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StaminaText == null )
     			{
		    		this.m_E_StaminaText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/AddPointBg/Stamina/E_Stamina");
     			}
     			return this.m_E_StaminaText;
     		}
     	}

		public UnityEngine.UI.Button E_AddStaminaButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddStaminaButton == null )
     			{
		    		this.m_E_AddStaminaButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Background/AddPointBg/Stamina/E_AddStamina");
     			}
     			return this.m_E_AddStaminaButton;
     		}
     	}

		public UnityEngine.UI.Image E_AddStaminaImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddStaminaImage == null )
     			{
		    		this.m_E_AddStaminaImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Background/AddPointBg/Stamina/E_AddStamina");
     			}
     			return this.m_E_AddStaminaImage;
     		}
     	}

		public UnityEngine.UI.Text E_AgilityText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AgilityText == null )
     			{
		    		this.m_E_AgilityText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/AddPointBg/Agility/E_Agility");
     			}
     			return this.m_E_AgilityText;
     		}
     	}

		public UnityEngine.UI.Button E_AddAgilityButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddAgilityButton == null )
     			{
		    		this.m_E_AddAgilityButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Background/AddPointBg/Agility/E_AddAgility");
     			}
     			return this.m_E_AddAgilityButton;
     		}
     	}

		public UnityEngine.UI.Image E_AddAgilityImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddAgilityImage == null )
     			{
		    		this.m_E_AddAgilityImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Background/AddPointBg/Agility/E_AddAgility");
     			}
     			return this.m_E_AddAgilityImage;
     		}
     	}

		public UnityEngine.UI.Text E_SpiritText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SpiritText == null )
     			{
		    		this.m_E_SpiritText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/AddPointBg/Spirit/E_Spirit");
     			}
     			return this.m_E_SpiritText;
     		}
     	}

		public UnityEngine.UI.Button E_AddSpiritButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddSpiritButton == null )
     			{
		    		this.m_E_AddSpiritButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Background/AddPointBg/Spirit/E_AddSpirit");
     			}
     			return this.m_E_AddSpiritButton;
     		}
     	}

		public UnityEngine.UI.Image E_AddSpiritImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddSpiritImage == null )
     			{
		    		this.m_E_AddSpiritImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Background/AddPointBg/Spirit/E_AddSpirit");
     			}
     			return this.m_E_AddSpiritImage;
     		}
     	}

		public UnityEngine.UI.Text E_DamageText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DamageText == null )
     			{
		    		this.m_E_DamageText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/StatusBg/AllStatus/Damage/E_Damage");
     			}
     			return this.m_E_DamageText;
     		}
     	}

		public UnityEngine.UI.Text E_DamageAddText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DamageAddText == null )
     			{
		    		this.m_E_DamageAddText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/StatusBg/AllStatus/DamageAdd/E_DamageAdd");
     			}
     			return this.m_E_DamageAddText;
     		}
     	}

		public UnityEngine.UI.Text E_HealthText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HealthText == null )
     			{
		    		this.m_E_HealthText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/StatusBg/AllStatus/Health/E_Health");
     			}
     			return this.m_E_HealthText;
     		}
     	}

		public UnityEngine.UI.Text E_MPText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MPText == null )
     			{
		    		this.m_E_MPText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/StatusBg/AllStatus/MP/E_MP");
     			}
     			return this.m_E_MPText;
     		}
     	}

		public UnityEngine.UI.Text E_ArmorText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ArmorText == null )
     			{
		    		this.m_E_ArmorText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/StatusBg/AllStatus/Armor/E_Armor");
     			}
     			return this.m_E_ArmorText;
     		}
     	}

		public UnityEngine.UI.Text E_ArmorAddText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ArmorAddText == null )
     			{
		    		this.m_E_ArmorAddText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/StatusBg/AllStatus/ArmorAdd/E_ArmorAdd");
     			}
     			return this.m_E_ArmorAddText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_CloseButton = null;
			this.m_EButton_CloseImage = null;
			this.m_EquipmentBgImage = null;
			this.m_E_CompatPowerText = null;
			this.m_E_AvailablePointText = null;
			this.m_E_StrengthText = null;
			this.m_E_AddStrengthButton = null;
			this.m_E_AddStrengthImage = null;
			this.m_E_StaminaText = null;
			this.m_E_AddStaminaButton = null;
			this.m_E_AddStaminaImage = null;
			this.m_E_AgilityText = null;
			this.m_E_AddAgilityButton = null;
			this.m_E_AddAgilityImage = null;
			this.m_E_SpiritText = null;
			this.m_E_AddSpiritButton = null;
			this.m_E_AddSpiritImage = null;
			this.m_E_DamageText = null;
			this.m_E_DamageAddText = null;
			this.m_E_HealthText = null;
			this.m_E_MPText = null;
			this.m_E_ArmorText = null;
			this.m_E_ArmorAddText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButton_CloseButton = null;
		private UnityEngine.UI.Image m_EButton_CloseImage = null;
		private UnityEngine.UI.Image m_EquipmentBgImage = null;
		private UnityEngine.UI.Text m_E_CompatPowerText = null;
		private UnityEngine.UI.Text m_E_AvailablePointText = null;
		private UnityEngine.UI.Text m_E_StrengthText = null;
		private UnityEngine.UI.Button m_E_AddStrengthButton = null;
		private UnityEngine.UI.Image m_E_AddStrengthImage = null;
		private UnityEngine.UI.Text m_E_StaminaText = null;
		private UnityEngine.UI.Button m_E_AddStaminaButton = null;
		private UnityEngine.UI.Image m_E_AddStaminaImage = null;
		private UnityEngine.UI.Text m_E_AgilityText = null;
		private UnityEngine.UI.Button m_E_AddAgilityButton = null;
		private UnityEngine.UI.Image m_E_AddAgilityImage = null;
		private UnityEngine.UI.Text m_E_SpiritText = null;
		private UnityEngine.UI.Button m_E_AddSpiritButton = null;
		private UnityEngine.UI.Image m_E_AddSpiritImage = null;
		private UnityEngine.UI.Text m_E_DamageText = null;
		private UnityEngine.UI.Text m_E_DamageAddText = null;
		private UnityEngine.UI.Text m_E_HealthText = null;
		private UnityEngine.UI.Text m_E_MPText = null;
		private UnityEngine.UI.Text m_E_ArmorText = null;
		private UnityEngine.UI.Text m_E_ArmorAddText = null;
		public Transform uiTransform = null;
	}
}
