namespace ET
{
    public class TileDetails
    {
        public int GridX, GridY;
        public bool CanDig;
        public bool CanDropItem;
        public bool CanPlaceFurniture;
        public bool IsNPCObstacle;
        public int DaysSinceDug = -1;
        public int DaysSinceWatered = -1;
        public int SeedItemId = -1;
        public int GrowthDays = -1;
        public int DaysSinceLastHarvest = -1;
    }
}