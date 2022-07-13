
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
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

		public UnityEngine.UI.Button E_UpLevelButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpLevelButton == null )
     			{
		    		this.m_E_UpLevelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Background/EquipmentBg/E_UpLevel");
     			}
     			return this.m_E_UpLevelButton;
     		}
     	}

		public UnityEngine.UI.Image E_UpLevelImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpLevelImage == null )
     			{
		    		this.m_E_UpLevelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Background/EquipmentBg/E_UpLevel");
     			}
     			return this.m_E_UpLevelImage;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Head
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_head == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/EquipmentBg/ES_EquipItem_Head");
		    	   this.m_es_equipitem_head = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_head;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Clothes
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_clothes == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/EquipmentBg/ES_EquipItem_Clothes");
		    	   this.m_es_equipitem_clothes = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_clothes;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Shoes
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_shoes == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/EquipmentBg/ES_EquipItem_Shoes");
		    	   this.m_es_equipitem_shoes = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_shoes;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Ring
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_ring == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/EquipmentBg/ES_EquipItem_Ring");
		    	   this.m_es_equipitem_ring = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_ring;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Weapon
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_weapon == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/EquipmentBg/ES_EquipItem_Weapon");
		    	   this.m_es_equipitem_weapon = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_weapon;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Shield
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_shield == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/EquipmentBg/ES_EquipItem_Shield");
		    	   this.m_es_equipitem_shield = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_shield;
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

		public ESAttributeItem ESAttributeItem_Strength
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esattributeitem_strength == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/AddPointBg/ESAttributeItem_Strength");
		    	   this.m_esattributeitem_strength = this.AddChild<ESAttributeItem,Transform>(subTrans);
     			}
     			return this.m_esattributeitem_strength;
     		}
     	}

		public ESAttributeItem ESAttributeItem_Stamina
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esattributeitem_stamina == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/AddPointBg/ESAttributeItem_Stamina");
		    	   this.m_esattributeitem_stamina = this.AddChild<ESAttributeItem,Transform>(subTrans);
     			}
     			return this.m_esattributeitem_stamina;
     		}
     	}

		public ESAttributeItem ESAttributeItem_Agility
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esattributeitem_agility == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/AddPointBg/ESAttributeItem_Agility");
		    	   this.m_esattributeitem_agility = this.AddChild<ESAttributeItem,Transform>(subTrans);
     			}
     			return this.m_esattributeitem_agility;
     		}
     	}

		public ESAttributeItem ESAttributeItem_Spirit
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esattributeitem_spirit == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/AddPointBg/ESAttributeItem_Spirit");
		    	   this.m_esattributeitem_spirit = this.AddChild<ESAttributeItem,Transform>(subTrans);
     			}
     			return this.m_esattributeitem_spirit;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_AttributeStatusLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AttributeStatusLoopVerticalScrollRect == null )
     			{
		    		this.m_E_AttributeStatusLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"Background/StatusBg/AllStatus/E_AttributeStatus");
     			}
     			return this.m_E_AttributeStatusLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_CloseButton = null;
			this.m_EButton_CloseImage = null;
			this.m_EquipmentBgImage = null;
			this.m_E_CompatPowerText = null;
			this.m_E_UpLevelButton = null;
			this.m_E_UpLevelImage = null;
			this.m_es_equipitem_head?.Dispose();
			this.m_es_equipitem_head = null;
			this.m_es_equipitem_clothes?.Dispose();
			this.m_es_equipitem_clothes = null;
			this.m_es_equipitem_shoes?.Dispose();
			this.m_es_equipitem_shoes = null;
			this.m_es_equipitem_ring?.Dispose();
			this.m_es_equipitem_ring = null;
			this.m_es_equipitem_weapon?.Dispose();
			this.m_es_equipitem_weapon = null;
			this.m_es_equipitem_shield?.Dispose();
			this.m_es_equipitem_shield = null;
			this.m_E_AvailablePointText = null;
			this.m_esattributeitem_strength?.Dispose();
			this.m_esattributeitem_strength = null;
			this.m_esattributeitem_stamina?.Dispose();
			this.m_esattributeitem_stamina = null;
			this.m_esattributeitem_agility?.Dispose();
			this.m_esattributeitem_agility = null;
			this.m_esattributeitem_spirit?.Dispose();
			this.m_esattributeitem_spirit = null;
			this.m_E_AttributeStatusLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButton_CloseButton = null;
		private UnityEngine.UI.Image m_EButton_CloseImage = null;
		private UnityEngine.UI.Image m_EquipmentBgImage = null;
		private UnityEngine.UI.Text m_E_CompatPowerText = null;
		private UnityEngine.UI.Button m_E_UpLevelButton = null;
		private UnityEngine.UI.Image m_E_UpLevelImage = null;
		private ES_EquipItem m_es_equipitem_head = null;
		private ES_EquipItem m_es_equipitem_clothes = null;
		private ES_EquipItem m_es_equipitem_shoes = null;
		private ES_EquipItem m_es_equipitem_ring = null;
		private ES_EquipItem m_es_equipitem_weapon = null;
		private ES_EquipItem m_es_equipitem_shield = null;
		private UnityEngine.UI.Text m_E_AvailablePointText = null;
		private ESAttributeItem m_esattributeitem_strength = null;
		private ESAttributeItem m_esattributeitem_stamina = null;
		private ESAttributeItem m_esattributeitem_agility = null;
		private ESAttributeItem m_esattributeitem_spirit = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_AttributeStatusLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
