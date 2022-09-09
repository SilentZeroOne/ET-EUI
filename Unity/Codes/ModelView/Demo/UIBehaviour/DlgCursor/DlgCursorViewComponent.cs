
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgCursorViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_CursorImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CursorImage == null )
     			{
		    		this.m_E_CursorImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Cursor");
     			}
     			return this.m_E_CursorImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CursorImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_CursorImage = null;
		public Transform uiTransform = null;
		public RectTransform uiRectTransform = null;
	}
}
