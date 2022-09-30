using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class AStarComponent: Entity, IAwake, IDestroy
    {
        public string SceneName;
        
        public Dictionary<string, GridTile> GridTilesMap = new Dictionary<string, GridTile>();
        public Node StartNode;
        public Node TargetNode;
        public int GridWidth;
        public int GridHeight;
        public int OriginX;//起始点坐标x,y
        public int OriginY;

        public List<Node> OpenNodeList;//当前Node周围的8个Node
        public HashSet<Node> ClosedNodeList;//所以被选中的Node
        public bool PathFound;
    }
}