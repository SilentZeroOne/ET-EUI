
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgMatchViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_StartMatchButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartMatchButton == null )
     			{
		    		this.m_E_StartMatchButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_StartMatch");
     			}
     			return this.m_E_StartMatchButton;
     		}
     	}

		public UnityEngine.UI.Image E_StartMatchImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartMatchImage == null )
     			{
		    		this.m_E_StartMatchImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_StartMatch");
     			}
     			return this.m_E_StartMatchImage;
     		}
     	}

		public TMPro.TextMeshProUGUI E_StartMatchTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartMatchTextMeshProUGUI == null )
     			{
		    		this.m_E_StartMatchTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_StartMatch/E_StartMatch");
     			}
     			return this.m_E_StartMatchTextMeshProUGUI;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_StartMatchButton = null;
			this.m_E_StartMatchImage = null;
			this.m_E_StartMatchTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_StartMatchButton = null;
		private UnityEngine.UI.Image m_E_StartMatchImage = null;
		private TMPro.TextMeshProUGUI m_E_StartMatchTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
