
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ESAttributeItemAwakeSystem : AwakeSystem<ESAttributeItem,Transform> 
	{
		public override void Awake(ESAttributeItem self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESAttributeItemDestroySystem : DestroySystem<ESAttributeItem> 
	{
		public override void Destroy(ESAttributeItem self)
		{
			self.DestroyWidget();
		}
	}
}
