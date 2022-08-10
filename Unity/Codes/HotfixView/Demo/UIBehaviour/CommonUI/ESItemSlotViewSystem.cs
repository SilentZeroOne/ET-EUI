
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ESItemSlotAwakeSystem : AwakeSystem<ESItemSlot,Transform> 
	{
		public override void Awake(ESItemSlot self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESItemSlotDestroySystem : DestroySystem<ESItemSlot> 
	{
		public override void Destroy(ESItemSlot self)
		{
			self.DestroyWidget();
		}
	}
}
