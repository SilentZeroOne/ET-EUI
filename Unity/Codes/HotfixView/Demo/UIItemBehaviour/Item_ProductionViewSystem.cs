
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_ProductionDestroySystem : DestroySystem<Scroll_Item_Production> 
	{
		public override void Destroy( Scroll_Item_Production self )
		{
			self.DestroyWidget();
		}
	}
}
