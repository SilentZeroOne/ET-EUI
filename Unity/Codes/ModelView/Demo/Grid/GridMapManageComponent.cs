using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(GridTile))]
    public class GridMapManageComponent: Entity, IAwake, IDestroy
    {
        public Dictionary<string, GridTile> GridTilesMap = new Dictionary<string, GridTile>();

        public IMapDataLoader DataLoader;

        public Grid CurrentGrid;

        public bool MapDataLoaded { get; set; }

        public Tilemap DigTilemap;
        public Tilemap WaterTilemap;
        
        public RuleTile WaterTile;
        public RuleTile DigTile;

        public SavedMapData SavedMapData;
    }
}