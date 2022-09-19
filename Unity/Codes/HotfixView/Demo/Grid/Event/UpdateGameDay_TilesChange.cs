using ET.EventType;

namespace ET
{
    public class UpdateGameDay_TilesChange : AEvent<UpdateGameDay>
    {
        protected override void Run(UpdateGameDay a)
        {
            a.ZoneScene.CurrentScene().DaysSinceInThisScene++;
            a.ZoneScene.CurrentScene().GetComponent<GridMapManageComponent>().UpdateTileWithDayChange(a.Time, 1);
        }
    }
}