using System.Collections.Generic;

namespace ET
{
	public  class DlgRole :Entity,IAwake
	{

		public DlgRoleViewComponent View { get => this.Parent.GetComponent<DlgRoleViewComponent>();}

		public Dictionary<int, Scroll_Item_Role> ScrollItemRoles;

	}
}
