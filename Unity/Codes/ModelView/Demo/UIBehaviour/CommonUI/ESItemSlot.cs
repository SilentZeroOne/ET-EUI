
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class ESItemSlot : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy 
	{
		public long DataId {get;set;}
		public UnityEngine.UI.Button E_SlotButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SlotButton == null )
     			{
		    		this.m_E_SlotButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Slot");
     			}
     			return this.m_E_SlotButton;
     		}
     	}

		public UnityEngine.UI.Image E_SlotImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SlotImage == null )
     			{
		    		this.m_E_SlotImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Slot");
     			}
     			return this.m_E_SlotImage;
     		}
     	}

		public UnityEngine.UI.Image E_ItemImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ItemImage == null )
     			{
		    		this.m_E_ItemImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Slot/E_Item");
     			}
     			return this.m_E_ItemImage;
     		}
     	}

		public UnityEngine.EventSystems.EventTrigger E_ItemEventTrigger
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ItemEventTrigger == null )
     			{
		    		this.m_E_ItemEventTrigger = UIFindHelper.FindDeepChild<UnityEngine.EventSystems.EventTrigger>(this.uiTransform.gameObject,"E_Slot/E_Item");
     			}
     			return this.m_E_ItemEventTrigger;
     		}
     	}

		public TMPro.TextMeshProUGUI E_CountTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CountTextMeshProUGUI == null )
     			{
		    		this.m_E_CountTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_Count");
     			}
     			return this.m_E_CountTextMeshProUGUI;
     		}
     	}

		public UnityEngine.UI.Image E_HightLightImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HightLightImage == null )
     			{
		    		this.m_E_HightLightImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_HightLight");
     			}
     			return this.m_E_HightLightImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_SlotButton = null;
			this.m_E_SlotImage = null;
			this.m_E_ItemImage = null;
			this.m_E_ItemEventTrigger = null;
			this.m_E_CountTextMeshProUGUI = null;
			this.m_E_HightLightImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Button m_E_SlotButton = null;
		private UnityEngine.UI.Image m_E_SlotImage = null;
		private UnityEngine.UI.Image m_E_ItemImage = null;
		private UnityEngine.EventSystems.EventTrigger m_E_ItemEventTrigger = null;
		private TMPro.TextMeshProUGUI m_E_CountTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_HightLightImage = null;
		public Transform uiTransform = null;
	}
}
