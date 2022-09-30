using System;

namespace ET
{
    [EnableMethod]
    public class Node: Entity, IAwake, IAwake<int, int>, IDestroy, IComparable<Node>
    {
        public int X;
        public int Y;
        public int GCost = 0;//距离Start的距离
        public int HCost = 0;//距离End的距离
        public int FCost => this.GCost + this.HCost;
        public Node ParentNode;
        
        public int CompareTo(Node other)
        {
            //小于 -1，等于 0 ，大于 1
            int result = this.FCost.CompareTo(other.FCost);
            if (result == 0)
            {
                result = this.HCost.CompareTo(other.HCost);
            }

            return result;
        }
    }
}