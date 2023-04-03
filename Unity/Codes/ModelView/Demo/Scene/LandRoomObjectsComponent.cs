using System.Collections.Generic;
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

		//玩家Id和座位Index的对应关系
		public Dictionary<long, int> Seats = new();
	}
}