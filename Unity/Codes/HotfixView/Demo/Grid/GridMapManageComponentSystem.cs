using System;
using System.Collections.Generic;
using BM;
using UnityEngine;

namespace ET
{
    public class GridMapManageComponentAwakeSystem: AwakeSystem<GridMapManageComponent>
    {
        public override void Awake(GridMapManageComponent self)
        {
            self.MapDataLoaded = false;
            self.DataLoader = new MapDataLoader();
            self.DigTile = AssetComponent.Load<RuleTile>(BPath.Assets_Bundles_ResBundles_RuleTiles_DigTile__asset);
            self.WaterTile = AssetComponent.Load<RuleTile>(BPath.Assets_Bundles_ResBundles_RuleTiles_WaterTile__asset);
            self.LoadMapData();
        }
    }

    public class GridMapManageComponentDestroySystem: DestroySystem<GridMapManageComponent>
    {
        public override void Destroy(GridMapManageComponent self)
        {

        }
    }
    

    [FriendClass(typeof (GridMapManageComponent))]
    public static class GridMapManageComponentSystem
    {
        public static void LoadMapData(this GridMapManageComponent self)
        {
            List<byte[]> mapDatas = new List<byte[]>();
            var sceneName = self.GetParent<Scene>().Name;
            self.DataLoader.GetSceneMapDataBytes(mapDatas, sceneName);

            foreach (var mapData in mapDatas)
            {
                MapData data = ProtobufHelper.Deserialize<MapData>(mapData);
                if (data.Tiles != null && data.Tiles.Count > 0)
                {
                    self.FillTileDetailsMap(data);
                }
            }

            self.MapDataLoaded = true;
        }

        private static void FillTileDetailsMap(this GridMapManageComponent self, MapData data)
        {
            var sceneName = self.GetParent<Scene>().Name;
            foreach (var tile in data.Tiles)
            {
                var key = tile.TileCoordinate.X + "x" + tile.TileCoordinate.Y + "y" + sceneName;
                if (!self.TileDetailsMap.ContainsKey(key))
                {
                    TileDetails detail = new() { GridX = tile.TileCoordinate.X, GridY = tile.TileCoordinate.Y };
                    self.TileDetailsMap.Add(key, detail);
                }

                switch ((GridType) tile.GridType)
                {
                    case GridType.Digable:
                        self.TileDetailsMap[key].CanDig = tile.Value;
                        break;
                    case GridType.DropItem:
                        self.TileDetailsMap[key].CanDropItem = tile.Value;
                        break;
                    case GridType.PlaceFurniture:
                        self.TileDetailsMap[key].CanPlaceFurniture = tile.Value;
                        break;
                    case GridType.NPCObstacle:
                        self.TileDetailsMap[key].IsNPCObstacle = tile.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// 获取特定TileDetail
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key">x+y+SceneName</param>
        /// <returns></returns>
        public static TileDetails GetTileDetails(this GridMapManageComponent self, string key)
        {
            self.TileDetailsMap.TryGetValue(key, out var tileDetails);
            return tileDetails;
        }

        public static void SetDigTile(this GridMapManageComponent self, TileDetails tileDetail)
        {
            if (self.DigTilemap != null)
            {
                var pos = new Vector3Int(tileDetail.GridX, tileDetail.GridY, 0);
                self.DigTilemap.SetTile(pos, self.DigTile);
            }
        }
        
        public static void SetWaterTile(this GridMapManageComponent self, TileDetails tileDetail)
        {
            if (self.WaterTilemap != null)
            {
                var pos = new Vector3Int(tileDetail.GridX, tileDetail.GridY, 0);
                self.WaterTilemap.SetTile(pos, self.WaterTile);
            }
        }
    }
}