
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgLandLobbyViewComponentAwakeSystem : AwakeSystem<DlgLandLobbyViewComponent> 
	{
		public override void Awake(DlgLandLobbyViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgLandLobbyViewComponentDestroySystem : DestroySystem<DlgLandLobbyViewComponent> 
	{
		public override void Destroy(DlgLandLobbyViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
