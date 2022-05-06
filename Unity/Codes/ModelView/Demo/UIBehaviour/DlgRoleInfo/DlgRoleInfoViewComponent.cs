
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
		private UnityEngine.UI.Text m_E_AvailablePointText = null;
		private ESAttributeItem m_esattributeitem_strength = null;
		private ESAttributeItem m_esattributeitem_stamina = null;
		private ESAttributeItem m_esattributeitem_agility = null;
		private ESAttributeItem m_esattributeitem_spirit = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_AttributeStatusLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
