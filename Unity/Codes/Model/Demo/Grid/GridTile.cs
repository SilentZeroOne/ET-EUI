namespace ET
{
    [ChildType()]
    public class GridTile: Entity, IAwake,IAwake<int, int>, IDestroy
    {
        public Crop Crop;
        public Node Node;
        public int GridX;
        public int GridY;
        public bool CanDig;
        public bool CanDropItem;
        public bool CanPlaceFurniture;
        public bool IsNPCObstacle;
        public int DaysSinceDug;
        public int DaysSinceWatered;
        public int GrowthDays;
        public int DaysSinceLastHarvest;
    }
}