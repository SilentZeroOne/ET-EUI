using System;

namespace ET
{
    public class NodeAwakeSystem: AwakeSystem<Node,int,int>
    {
        public override void Awake(Node self, int x, int y)
        {
            self.ParentNode = null;
            self.X = x;
            self.Y = y;
        }
    }

    public class NodeDestroySystem: DestroySystem<Node>
    {
        public override void Destroy(Node self)
        {

        }
    }

    [FriendClass(typeof (Node))]
    public static class NodeSystem
    {
        public static int GetDistance(this Node self, Node other)
        {
            int xDistance = Math.Abs(self.X - other.X);
            int yDistance = Math.Abs(self.Y - other.Y);

            if (xDistance > yDistance)
            {
                return 10 * (xDistance - yDistance) + yDistance * 14;
            }

            return 10 * (yDistance - xDistance) + xDistance * 14;
        }
        
    }
}