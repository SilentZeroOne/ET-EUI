
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ES_PlayerStatusUIAwakeSystem : AwakeSystem<ES_PlayerStatusUI,Transform> 
	{
		public override void Awake(ES_PlayerStatusUI self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ES_PlayerStatusUIDestroySystem : DestroySystem<ES_PlayerStatusUI> 
	{
		public override void Destroy(ES_PlayerStatusUI self)
		{
			self.DestroyWidget();
		}
	}
}
