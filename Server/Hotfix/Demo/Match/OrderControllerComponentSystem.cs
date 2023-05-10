using System;
using System.Collections.Generic;

namespace ET
{
	public class OrderControllerComponentAwakeSystem: AwakeSystem<OrderControllerComponent>
	{
		public override void Awake(OrderControllerComponent self)
		{
			self.CurrentPlayer = 0;
			self.Biggest = 0;
			self.SelectLordIndex = 1;
			self.GameLordState.Clear();
		}
	}

	public class OrderControllerComponentDestroySystem: DestroySystem<OrderControllerComponent>
	{
		public override void Destroy(OrderControllerComponent self)
		{

		}
	}

    [FriendClass(typeof(OrderControllerComponent))]
    [FriendClassAttribute(typeof(ET.Room))]
    public static class OrderControllerComponentSystem
    {
        public static void Init(this OrderControllerComponent self, long unitId)
        {
            self.FirstAuthority = new KeyValuePair<long, bool>(unitId, false);
            self.CurrentPlayer = 0;
            self.Biggest = 0;
            self.SelectLordIndex = 1;
            self.GameLordState.Clear();
        }

        public static void Turn(this OrderControllerComponent self)
        {
            Room room = self.GetParent<Room>();
            var players = room.Units;
            int index = Array.FindIndex(players, (player) => player.Id == self.CurrentPlayer);
            index++;
            if (index >= players.Length)
            {
	            index = 0;
            }

            self.CurrentPlayer = players[index].Id;
        }
    }
}