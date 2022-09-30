using System;
using System.Collections.Generic;

namespace ET
{
    public class AStarComponentAwakeSystem: AwakeSystem<AStarComponent>
    {
        public override void Awake(AStarComponent self)
        {
            self.SceneName = self.GetParent<Scene>().Name;
            self.OpenNodeList = new List<Node>();
            self.ClosedNodeList = new HashSet<Node>();
        }
    }

    public class AStarComponentDestroySystem: DestroySystem<AStarComponent>
    {
        public override void Destroy(AStarComponent self)
        {

        }
    }

    [FriendClass(typeof(AStarComponent))]
    [FriendClassAttribute(typeof(ET.GridTile))]
    [FriendClassAttribute(typeof(ET.Node))]
    public static class AStarComponentSystem
    {
        /// <summary>
        /// 必须执行Init初始化
        /// </summary>
        public static void Init(this AStarComponent self, int width, int height, int originX, int originY, Dictionary<string, GridTile> data)
        {
            self.GridWidth = width;
            self.GridHeight = height;
            self.OriginX = originX;
            self.OriginY = originY;
            self.GridTilesMap = data;
        }

        public static void BuildPath(this AStarComponent self, int startX, int startY, int endX, int endY,Stack<MovementStep> stack)
        {
            self.PathFound = false;

            self.OpenNodeList.Clear();
            self.ClosedNodeList.Clear();
            
            self.StartNode = self.GetGridTile(startX, startY).Node;
            self.TargetNode = self.GetGridTile(endX, endY).Node;

            //寻找最短路径
            if (self.FindShortestPath())
            {
                //构建移动路径
                self.UpdateMovementStep(stack);
            }
        }

        /// <summary>
        /// 获取特定TileDetail
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key">x+y+SceneName</param>
        /// <returns></returns>
        public static GridTile GetGridTile(this AStarComponent self, string key)
        {
            self.GridTilesMap.TryGetValue(key, out var gridTile);
            return gridTile;
        }

        public static GridTile GetGridTile(this AStarComponent self, int x, int y)
        {
            var currentName = self.GetParent<Scene>().Name;
            var key = $"{x}x{y}y{currentName}";
            return self.GetGridTile(key);
        }

        public static bool FindShortestPath(this AStarComponent self)
        {
            self.OpenNodeList.Add(self.StartNode);

            while (self.OpenNodeList.Count > 0)
            {
                //对节点进行排序 最小的在第一个 Node内有Compare函数
                self.OpenNodeList.Sort();

                Node closeNode = self.OpenNodeList[0];

                self.OpenNodeList.RemoveAt(0);
                self.ClosedNodeList.Add(closeNode);

                if (closeNode == self.TargetNode)
                {
                    self.PathFound = true;
                    break;
                }

                //计算周围8个Node 补充到OpenNodeList中
                self.EvaluateNeighbourNodes(closeNode);
            }

            return self.PathFound;
        }

        /// <summary>
        /// 评估周围8个点，计算cost
        /// </summary>
        /// <param name="self"></param>
        /// <param name="currentNode"></param>
        public static void EvaluateNeighbourNodes(this AStarComponent self, Node currentNode)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;//跳过自己
                    var vaildNode = self.GetVaildNeighbourNode(currentNode.X + x, currentNode.Y + y);
                    if (vaildNode != null)
                    {
                        if (!self.OpenNodeList.Contains(vaildNode))
                        {
                            vaildNode.GCost = currentNode.GCost + currentNode.GetDistance(vaildNode);
                            vaildNode.HCost = vaildNode.GetDistance(self.TargetNode);
                            //链接父节点
                            vaildNode.ParentNode = currentNode;
                        
                            self.OpenNodeList.Add(vaildNode);
                        }
                    }
                    else
                    {
                        Log.Debug($"Invaild Node x:{x} y:{y}");
                    }
                }
            }
        }

        public static Node GetVaildNeighbourNode(this AStarComponent self, int x, int y)
        {
            if (x < self.OriginX || y < self.OriginY || x > self.GridWidth + self.OriginX || y > self.GridHeight + self.OriginY)
            {
                return null;
            }

            var tile = self.GetGridTile(x, y);
            if (tile != null)
            {
                if (tile.IsNPCObstacle || self.ClosedNodeList.Contains(tile.Node))
                {
                    return null;
                }
            }
            else
            {
                Log.Debug($"x{x} y{y} is null");
                return null;
            }
            
            return tile.Node;
        }

        public static void UpdateMovementStep(this AStarComponent self,Stack<MovementStep> stack)
        {
            Node nextNode = self.TargetNode;

            while (nextNode.ParentNode != null)
            {
                MovementStep step = new MovementStep();
                step.GridX = nextNode.X;
                step.GridY = nextNode.Y;
                stack.Push(step);

                nextNode = nextNode.ParentNode;
            }
        }
    }
}