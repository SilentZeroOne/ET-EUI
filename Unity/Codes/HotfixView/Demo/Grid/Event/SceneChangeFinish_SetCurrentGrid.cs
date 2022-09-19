using ET.EventType;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ET
{
    [FriendClassAttribute(typeof(ET.GridMapManageComponent))]
    public class SceneChangeFinish_SetCurrentGrid : AEventAsync<SceneChangeFinish>
    {
        protected override async ETTask Run(SceneChangeFinish a)
        {
            var gridMapManager = a.CurrentScene.GetComponent<GridMapManageComponent>();
            gridMapManager.CurrentGrid = UnityEngine.Object.FindObjectOfType<Grid>();
            gridMapManager.DigTilemap = GameObject.FindGameObjectWithTag(TagManager.DigMap).GetComponent<Tilemap>();
            gridMapManager.WaterTilemap = GameObject.FindGameObjectWithTag(TagManager.WaterMap).GetComponent<Tilemap>();
            gridMapManager.UpdateTileWithDayChange(null, a.CurrentScene.DaysSinceInThisScene, false);
            gridMapManager.DisplayMap();
            
            a.CurrentScene.DaysSinceInThisScene = 0;
            await ETTask.CompletedTask;
        }
    }
}