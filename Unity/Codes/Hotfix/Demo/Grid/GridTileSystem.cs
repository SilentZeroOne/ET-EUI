using ET.EventType;

namespace ET
{
    public class GridTileAwakeSystem: AwakeSystem<GridTile,int,int>
    {
        public override void Awake(GridTile self, int x, int y)
        {
            self.GridX = x;
            self.GridY = y;
            self.DaysSinceDug = -1;
            self.DaysSinceWatered = -1;
            self.DaysSinceLastHarvest = -1;
            self.GrowthDays = -1;
        }
    }

    public class GridTileDestroySystem: DestroySystem<GridTile>
    {
        public override void Destroy(GridTile self)
        {

        }
    }

    [FriendClass(typeof (GridTile))]
    public static class GridTileSystem
    {
        public static void Test(this GridTile self)
        {
        }

        public static void AddCrop(this GridTile self, int cropConfigId)
        {
            self.Crop = self.AddChild<Crop, int>(cropConfigId);
            self.GrowthDays = 0;

            Game.EventSystem.Publish(new AfterCropCreate() { Crop = self.Crop, ForceDisplay = true });
        }

        public static void Drag(this GridTile self)
        {
            self.CanDig = false;
            self.DaysSinceDug = 0;
            self.CanDropItem = false;
        }

        public static void FromProto(this GridTile self, TileDetails proto)
        {
            self.GridX = proto.GridX;
            self.GridY = proto.GridY;
            self.CanDig = proto.CanDig;
            self.CanDropItem = proto.CanDropItem;
            self.CanPlaceFurniture = proto.CanPlaceFurniture;
            self.IsNPCObstacle = proto.IsNPCObstacle;
            self.GrowthDays = proto.GrowthDays;
            self.DaysSinceDug = proto.DaysSinceDug;
            self.DaysSinceWatered = proto.DaysSinceWatered;
            self.DaysSinceLastHarvest = proto.DaysSinceLastHarvest;

            if (proto.Crop != null)
            {
                self.Crop = self.AddChildWithId<Crop>(proto.Crop.CropId);
                self.Crop.FromProto(proto.Crop);

                Game.EventSystem.Publish(new AfterCropCreate() { Crop = self.Crop, ForceDisplay = true });
            }
        }

        public static TileDetails ToProto(this GridTile self)
        {
            return new TileDetails()
            {
                GridX = self.GridX,
                GridY = self.GridY,
                CanDig = self.CanDig,
                CanDropItem = self.CanDropItem,
                CanPlaceFurniture = self.CanPlaceFurniture,
                Crop = self.Crop.ToProto(),
                DaysSinceDug = self.DaysSinceDug,
                DaysSinceWatered = self.DaysSinceWatered,
                DaysSinceLastHarvest = self.DaysSinceLastHarvest,
                GrowthDays = self.GrowthDays,
                IsNPCObstacle = self.IsNPCObstacle,
            };
        }
    }
}