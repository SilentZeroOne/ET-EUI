
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_LevelDetailDestroySystem : DestroySystem<Scroll_Item_LevelDetail> 
	{
		public override void Destroy( Scroll_Item_LevelDetail self )
		{
			self.DestroyWidget();
		}
	}
}
