
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgMainViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_InventoryButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InventoryButton == null )
     			{
		    		this.m_E_InventoryButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"ActionBar/E_Inventory");
     			}
     			return this.m_E_InventoryButton;
     		}
     	}

		public UnityEngine.UI.Image E_InventoryImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InventoryImage == null )
     			{
		    		this.m_E_InventoryImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ActionBar/E_Inventory");
     			}
     			return this.m_E_InventoryImage;
     		}
     	}

		public ESItemSlot ESItemSlot
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esitemslot == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ActionBar/ESItemSlot");
		    	   this.m_esitemslot = this.AddChild<ESItemSlot,Transform>(subTrans);
     			}
     			return this.m_esitemslot;
     		}
     	}

		public ESItemSlot ESItemSlot1
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esitemslot1 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ActionBar/ESItemSlot1");
		    	   this.m_esitemslot1 = this.AddChild<ESItemSlot,Transform>(subTrans);
     			}
     			return this.m_esitemslot1;
     		}
     	}

		public ESItemSlot ESItemSlot2
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esitemslot2 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ActionBar/ESItemSlot2");
		    	   this.m_esitemslot2 = this.AddChild<ESItemSlot,Transform>(subTrans);
     			}
     			return this.m_esitemslot2;
     		}
     	}

		public ESItemSlot ESItemSlot3
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esitemslot3 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ActionBar/ESItemSlot3");
		    	   this.m_esitemslot3 = this.AddChild<ESItemSlot,Transform>(subTrans);
     			}
     			return this.m_esitemslot3;
     		}
     	}

		public ESItemSlot ESItemSlot4
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esitemslot4 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ActionBar/ESItemSlot4");
		    	   this.m_esitemslot4 = this.AddChild<ESItemSlot,Transform>(subTrans);
     			}
     			return this.m_esitemslot4;
     		}
     	}

		public ESItemSlot ESItemSlot5
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esitemslot5 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ActionBar/ESItemSlot5");
		    	   this.m_esitemslot5 = this.AddChild<ESItemSlot,Transform>(subTrans);
     			}
     			return this.m_esitemslot5;
     		}
     	}

		public ESItemSlot ESItemSlot6
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esitemslot6 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ActionBar/ESItemSlot6");
		    	   this.m_esitemslot6 = this.AddChild<ESItemSlot,Transform>(subTrans);
     			}
     			return this.m_esitemslot6;
     		}
     	}

		public ESItemSlot ESItemSlot7
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esitemslot7 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ActionBar/ESItemSlot7");
		    	   this.m_esitemslot7 = this.AddChild<ESItemSlot,Transform>(subTrans);
     			}
     			return this.m_esitemslot7;
     		}
     	}

		public ESItemSlot ESItemSlot8
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esitemslot8 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ActionBar/ESItemSlot8");
		    	   this.m_esitemslot8 = this.AddChild<ESItemSlot,Transform>(subTrans);
     			}
     			return this.m_esitemslot8;
     		}
     	}

		public ESItemSlot ESItemSlot9
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esitemslot9 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ActionBar/ESItemSlot9");
		    	   this.m_esitemslot9 = this.AddChild<ESItemSlot,Transform>(subTrans);
     			}
     			return this.m_esitemslot9;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_InventoryButton = null;
			this.m_E_InventoryImage = null;
			this.m_esitemslot?.Dispose();
			this.m_esitemslot = null;
			this.m_esitemslot1?.Dispose();
			this.m_esitemslot1 = null;
			this.m_esitemslot2?.Dispose();
			this.m_esitemslot2 = null;
			this.m_esitemslot3?.Dispose();
			this.m_esitemslot3 = null;
			this.m_esitemslot4?.Dispose();
			this.m_esitemslot4 = null;
			this.m_esitemslot5?.Dispose();
			this.m_esitemslot5 = null;
			this.m_esitemslot6?.Dispose();
			this.m_esitemslot6 = null;
			this.m_esitemslot7?.Dispose();
			this.m_esitemslot7 = null;
			this.m_esitemslot8?.Dispose();
			this.m_esitemslot8 = null;
			this.m_esitemslot9?.Dispose();
			this.m_esitemslot9 = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_InventoryButton = null;
		private UnityEngine.UI.Image m_E_InventoryImage = null;
		private ESItemSlot m_esitemslot = null;
		private ESItemSlot m_esitemslot1 = null;
		private ESItemSlot m_esitemslot2 = null;
		private ESItemSlot m_esitemslot3 = null;
		private ESItemSlot m_esitemslot4 = null;
		private ESItemSlot m_esitemslot5 = null;
		private ESItemSlot m_esitemslot6 = null;
		private ESItemSlot m_esitemslot7 = null;
		private ESItemSlot m_esitemslot8 = null;
		private ESItemSlot m_esitemslot9 = null;
		public Transform uiTransform = null;
	}
}
