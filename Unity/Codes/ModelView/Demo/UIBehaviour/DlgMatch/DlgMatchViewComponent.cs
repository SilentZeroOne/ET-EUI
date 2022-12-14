
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

		public UnityEngine.RectTransform EG_MatchingRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_MatchingRectTransform == null )
     			{
		    		this.m_EG_MatchingRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_Matching");
     			}
     			return this.m_EG_MatchingRectTransform;
     		}
     	}

		public TMPro.TextMeshProUGUI E_MatchingCountTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MatchingCountTextMeshProUGUI == null )
     			{
		    		this.m_E_MatchingCountTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_Matching/E_MatchingCount");
     			}
     			return this.m_E_MatchingCountTextMeshProUGUI;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_StartMatchButton = null;
			this.m_E_StartMatchImage = null;
			this.m_E_StartMatchTextMeshProUGUI = null;
			this.m_EG_MatchingRectTransform = null;
			this.m_E_MatchingCountTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_StartMatchButton = null;
		private UnityEngine.UI.Image m_E_StartMatchImage = null;
		private TMPro.TextMeshProUGUI m_E_StartMatchTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_MatchingRectTransform = null;
		private TMPro.TextMeshProUGUI m_E_MatchingCountTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
