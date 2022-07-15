using System.Collections.Generic;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgForge :Entity,IAwake,IUILogic
	{

		public DlgForgeViewComponent View { get => this.Parent.GetComponent<DlgForgeViewComponent>();}

		public Dictionary<int, Scroll_Item_Production> ScrollItemProductions = new Dictionary<int, Scroll_Item_Production>();

		public long MakeQueueTimer = 0;
	}
}
