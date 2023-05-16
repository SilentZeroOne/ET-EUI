using System.Collections.Generic;

namespace ET
{
	[ComponentOf()]
	public class LandInteractionComponent: Entity, IAwake, IDestroy
	{
		public List<long> SelectedCard = new(20);
	}
}