
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_StatusDestroySystem : DestroySystem<Scroll_Item_Status> 
	{
		public override void Destroy( Scroll_Item_Status self )
		{
			self.DestroyWidget();
		}
	}
}
