
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class Scroll_Item_InventorySlot : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_InventorySlot BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
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
     			if (this.isCacheNode)
     			{
     				if( this.m_E_SlotImage == null )
     				{
		    			this.m_E_SlotImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ItemSlot/E_Slot");
     				}
     				return this.m_E_SlotImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ItemSlot/E_Slot");
     			}
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
     			if (this.isCacheNode)
     			{
     				if( this.m_E_ItemImage == null )
     				{
		    			this.m_E_ItemImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ItemSlot/E_Slot/E_Item");
     				}
     				return this.m_E_ItemImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ItemSlot/E_Slot/E_Item");
     			}
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
     			if (this.isCacheNode)
     			{
     				if( this.m_E_ItemEventTrigger == null )
     				{
		    			this.m_E_ItemEventTrigger = UIFindHelper.FindDeepChild<UnityEngine.EventSystems.EventTrigger>(this.uiTransform.gameObject,"ItemSlot/E_Slot/E_Item");
     				}
     				return this.m_E_ItemEventTrigger;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.EventSystems.EventTrigger>(this.uiTransform.gameObject,"ItemSlot/E_Slot/E_Item");
     			}
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
     			if (this.isCacheNode)
     			{
     				if( this.m_E_CountTextMeshProUGUI == null )
     				{
		    			this.m_E_CountTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"ItemSlot/E_Count");
     				}
     				return this.m_E_CountTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"ItemSlot/E_Count");
     			}
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
     			if (this.isCacheNode)
     			{
     				if( this.m_E_HightLightImage == null )
     				{
		    			this.m_E_HightLightImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ItemSlot/E_HightLight");
     				}
     				return this.m_E_HightLightImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ItemSlot/E_HightLight");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_SlotImage = null;
			this.m_E_ItemImage = null;
			this.m_E_ItemEventTrigger = null;
			this.m_E_CountTextMeshProUGUI = null;
			this.m_E_HightLightImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Image m_E_SlotImage = null;
		private UnityEngine.UI.Image m_E_ItemImage = null;
		private UnityEngine.EventSystems.EventTrigger m_E_ItemEventTrigger = null;
		private TMPro.TextMeshProUGUI m_E_CountTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_HightLightImage = null;
		public Transform uiTransform = null;
	}
}
