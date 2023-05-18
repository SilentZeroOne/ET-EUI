
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class ES_PlayerStatusUI : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy 
	{
		public TMPro.TextMeshProUGUI E_NickNameTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NickNameTextMeshProUGUI == null )
     			{
		    		this.m_E_NickNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_NickName");
     			}
     			return this.m_E_NickNameTextMeshProUGUI;
     		}
     	}

		public TMPro.TextMeshProUGUI E_HuanLeDouTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HuanLeDouTextMeshProUGUI == null )
     			{
		    		this.m_E_HuanLeDouTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_HuanLeDou");
     			}
     			return this.m_E_HuanLeDouTextMeshProUGUI;
     		}
     	}

		public UnityEngine.UI.Image E_BeanImageImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BeanImageImage == null )
     			{
		    		this.m_E_BeanImageImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_HuanLeDou/E_BeanImage");
     			}
     			return this.m_E_BeanImageImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_NickNameTextMeshProUGUI = null;
			this.m_E_HuanLeDouTextMeshProUGUI = null;
			this.m_E_BeanImageImage = null;
			this.uiTransform = null;
		}

		private TMPro.TextMeshProUGUI m_E_NickNameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_HuanLeDouTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_BeanImageImage = null;
		public Transform uiTransform = null;
	}
}
