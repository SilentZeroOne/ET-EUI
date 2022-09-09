using UnityEngine;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgCursor :Entity,IAwake,IUILogic,IUpdate
	{

		public DlgCursorViewComponent View { get => this.Parent.GetComponent<DlgCursorViewComponent>();} 

		public Sprite DefaultCursor { get; set; }
		public Sprite CurrentCursor { get; set; }

	}
}
