namespace ET
{
	public class LandInteractionComponentAwakeSystem: AwakeSystem<LandInteractionComponent>
	{
		public override void Awake(LandInteractionComponent self)
		{

		}
	}

	public class LandInteractionComponentDestroySystem: DestroySystem<LandInteractionComponent>
	{
		public override void Destroy(LandInteractionComponent self)
		{

		}
	}

	[FriendClass(typeof (LandInteractionComponent))]
	public static class LandInteractionComponentSystem
	{
		public static void AddOrRemoveSelectedCard(this LandInteractionComponent self, long cardId, bool selected)
		{
			if (selected)
				self.SelectedCard.Add(cardId);
			else
			{
				if (self.SelectedCard.Contains(cardId))
				{
					self.SelectedCard.Remove(cardId);
				}
			}
		}

		public static void ClearSelectedCard(this LandInteractionComponent self)
		{
			self.SelectedCard.Clear();
		}
	}
}