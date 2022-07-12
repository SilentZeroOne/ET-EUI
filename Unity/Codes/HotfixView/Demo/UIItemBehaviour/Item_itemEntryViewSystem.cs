
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_itemEntryDestroySystem : DestroySystem<Scroll_Item_itemEntry> 
	{
		public override void Destroy( Scroll_Item_itemEntry self )
		{
			self.DestroyWidget();
		}
	}
}
