
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgDragItemViewComponent : Entity,IAwake,IDestroy 
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

		public void DestroyWidget()
		{
			this.m_E_ItemImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_ItemImage = null;
		public Transform uiTransform = null;
	}
}
