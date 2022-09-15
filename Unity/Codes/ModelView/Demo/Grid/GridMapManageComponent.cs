using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class GridMapManageComponent: Entity, IAwake, IDestroy
    {
        public Dictionary<string, TileDetails> TileDetailsMap = new Dictionary<string, TileDetails>();

        public IMapDataLoader DataLoader;

        public Grid CurrentGrid;

        public bool MapDataLoaded { get; set; }

        public Tilemap DigTilemap;
        public Tilemap WaterTilemap;
        
        public RuleTile WaterTile;
        public RuleTile DigTile;
    }
}