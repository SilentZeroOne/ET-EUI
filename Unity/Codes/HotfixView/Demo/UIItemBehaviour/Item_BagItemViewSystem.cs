
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_BagItemDestroySystem : DestroySystem<Scroll_Item_BagItem> 
	{
		public override void Destroy( Scroll_Item_BagItem self )
		{
			self.DestroyWidget();
		}
	}
}
