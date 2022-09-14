using UnityEngine;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgCursor :Entity,IAwake,IUILogic,IUpdate
	{

		public DlgCursorViewComponent View { get => this.Parent.GetComponent<DlgCursorViewComponent>();} 

		public Sprite DefaultCursor { get; set; }
		public Sprite CurrentCursor { get; set; }

		public Item CurrentItem;

		private bool _cursorEnable;
		public bool CursorEnable
		{
			get => _cursorEnable;
			set
			{
				this._cursorEnable = value;
				this.View.E_CursorImage.color = value? Color.white : new Color(1, 0, 0, 0.4f);
			}
		}
	}
}
