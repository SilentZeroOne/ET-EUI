using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(Room))]
    public class LandMatchComponent: Entity, IAwake, IDestroy
    {
        /// <summary>
        /// 游戏中的房间
        /// </summary>
        public readonly Dictionary<long, Room> GamingLandlordsDict = new Dictionary<long, Room>();

        /// <summary>
        /// 还没开始的房间 RoomId/Room
        /// </summary>
        public readonly Dictionary<long, Room> FreeLandlordsDict = new Dictionary<long, Room>();

        /// <summary>
        /// 等待中的Unit   Unitid/Room
        /// </summary>
        public readonly Dictionary<long, Room> WaitingUnit = new Dictionary<long, Room>();

        /// <summary>
        /// 游玩中的Unit
        /// </summary>
        public readonly Dictionary<long, Room> PlayingUnit = new Dictionary<long, Room>();

        /// <summary>
        /// 匹配队列
        /// </summary>
        public readonly Queue<Unit> MatchingQueue = new Queue<Unit>();

    }
}