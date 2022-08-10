
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class ESItemSlot : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy 
	{
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
		    		this.m_E_ItemImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Item");
     			}
     			return this.m_E_ItemImage;
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

		public void DestroyWidget()
		{
			this.m_E_ItemImage = null;
			this.m_E_CountTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_ItemImage = null;
		private TMPro.TextMeshProUGUI m_E_CountTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
