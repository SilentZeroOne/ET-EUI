using UnityEngine;

namespace ET
{
	[ComponentOf()]
	public class LandRoomObjectsComponent: Entity, IAwake, IDestroy
	{
		public SpriteRenderer DeskSprite;
		
		public Transform[] OtherUnitPositions;

		public Transform SelfUnitPosition;

		public bool[] PositionUsed = new[] { false, false };
	}
}