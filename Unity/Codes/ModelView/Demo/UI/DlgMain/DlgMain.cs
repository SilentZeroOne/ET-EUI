using System.Collections.Generic;
using UnityEngine;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgMain :Entity,IAwake,IUILogic
	{
		public DlgMainViewComponent View { get => this.Parent.GetComponent<DlgMainViewComponent>();}

		public List<RectTransform> ReadyIcon = new List<RectTransform>(3);
	}
}
