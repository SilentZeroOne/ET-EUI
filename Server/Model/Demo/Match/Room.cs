using System.Collections.Generic;

namespace ET
{
    public class Room: Entity, IAwake, IDestroy
    {
        public readonly Dictionary<long, int> Seats = new Dictionary<long, int>(3);

        public readonly Unit[] Units = new Unit[3];

        public readonly bool[] isReady = { false, false, false };

        public int PlayerCount => this.Seats.Values.Count;

        public Scene PlayingScene { get; set; }

        public long CloseTimer;
    }
}