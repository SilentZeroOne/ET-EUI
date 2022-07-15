
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgForgeViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_CloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseButton == null )
     			{
		    		this.m_E_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Background/E_Close");
     			}
     			return this.m_E_CloseButton;
     		}
     	}

		public UnityEngine.UI.Image E_CloseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseImage == null )
     			{
		    		this.m_E_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Background/E_Close");
     			}
     			return this.m_E_CloseImage;
     		}
     	}

		public ES_MakeQueue ES_MakeQueue1
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_makequeue1 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/LayoutGroup/MakeQueueGroup/ES_MakeQueue1");
		    	   this.m_es_makequeue1 = this.AddChild<ES_MakeQueue,Transform>(subTrans);
     			}
     			return this.m_es_makequeue1;
     		}
     	}

		public ES_MakeQueue ES_MakeQueue2
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_makequeue2 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"Background/LayoutGroup/MakeQueueGroup/ES_MakeQueue2");
		    	   this.m_es_makequeue2 = this.AddChild<ES_MakeQueue,Transform>(subTrans);
     			}
     			return this.m_es_makequeue2;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_ProductionLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ProductionLoopVerticalScrollRect == null )
     			{
		    		this.m_E_ProductionLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"Background/LayoutGroup/ProductionGroup/E_Production");
     			}
     			return this.m_E_ProductionLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Text E_IronStoneCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_IronStoneCountText == null )
     			{
		    		this.m_E_IronStoneCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/LayoutGroup/ButtomGroup/IronStoneTip/E_IronStoneCount");
     			}
     			return this.m_E_IronStoneCountText;
     		}
     	}

		public UnityEngine.UI.Text E_FurCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FurCountText == null )
     			{
		    		this.m_E_FurCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Background/LayoutGroup/ButtomGroup/FurTip/E_FurCount");
     			}
     			return this.m_E_FurCountText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.m_es_makequeue1?.Dispose();
			this.m_es_makequeue1 = null;
			this.m_es_makequeue2?.Dispose();
			this.m_es_makequeue2 = null;
			this.m_E_ProductionLoopVerticalScrollRect = null;
			this.m_E_IronStoneCountText = null;
			this.m_E_FurCountText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		private ES_MakeQueue m_es_makequeue1 = null;
		private ES_MakeQueue m_es_makequeue2 = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_ProductionLoopVerticalScrollRect = null;
		private UnityEngine.UI.Text m_E_IronStoneCountText = null;
		private UnityEngine.UI.Text m_E_FurCountText = null;
		public Transform uiTransform = null;
	}
}
