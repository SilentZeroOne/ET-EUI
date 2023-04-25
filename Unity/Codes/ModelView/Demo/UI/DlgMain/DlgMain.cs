using System.Collections.Generic;
using UnityEngine;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgMain :Entity,IAwake,IUILogic
	{
		public DlgMainViewComponent View { get => this.Parent.GetComponent<DlgMainViewComponent>();}

		public List<RectTransform> ReadyIcon = new(3);

		public Dictionary<long,GameObject> Cards = new(30);
	}
}
