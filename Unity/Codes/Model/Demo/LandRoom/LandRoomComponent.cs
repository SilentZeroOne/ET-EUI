using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class LandRoomComponent: Entity, IAwake, IDestroy
    {
        public readonly Dictionary<long, int> Seats = new Dictionary<long, int>(3);
        
        public readonly Unit[] Units = new Unit[3];
        
        public readonly bool[] isReady = { false, false, false };
        
        public int Multiples { get; set; }
        
        public int PlayerCount => this.Seats.Values.Count;

        //正在准备中--往服务器发送消息
        public bool InReady { get; set; }
        
        //自己已经准备好了
        public bool SelfIsReady { get; set; }
    }
}