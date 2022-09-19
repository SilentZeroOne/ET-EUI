using System;
using System.Collections.Generic;
using System.IO;
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
            self.LoadMapData().Coroutine();
        }
    }

    public class GridMapManageComponentDestroySystem: DestroySystem<GridMapManageComponent>
    {
        public override void Destroy(GridMapManageComponent self)
        {
            self.SaveMapData();
        }
    }
    

    [FriendClass(typeof (GridMapManageComponent))]
    public static class GridMapManageComponentSystem
    {
        public static async ETTask LoadMapData(this GridMapManageComponent self)
        {
            List<byte[]> mapDatas = new List<byte[]>();
            var sceneName = self.GetParent<Scene>().Name;
            self.SavedMapData = await self.DataLoader.GetSceneSavedMapData(sceneName);
            if (self.SavedMapData != null)//有存档 读取存档数据
            {
                self.FillTileDetailsMap();
            }
            else//读取初始数据
            {
                self.DataLoader.GetSceneMapDataBytes(mapDatas, sceneName);

                foreach (var mapData in mapDatas)
                {
                    MapData data = ProtobufHelper.Deserialize<MapData>(mapData);
                    if (data.Tiles != null && data.Tiles.Count > 0)
                    {
                        self.FillTileDetailsMap(data);
                    }
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
                    TileDetails detail = new() { 
                        GridX = tile.TileCoordinate.X, 
                        GridY = tile.TileCoordinate.Y,
                        DaysSinceDug = -1,
                        DaysSinceWatered = -1,
                        DaysSinceLastHarvest = -1,
                        GrowthDays = -1,
                        SeedItemId = -1
                    };
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

        private static void FillTileDetailsMap(this GridMapManageComponent self)
        {
            var sceneName = self.GetParent<Scene>().Name;
            foreach (var tile in self.SavedMapData.TileDetailsList)
            {
                var key = tile.GridX + "x" + tile.GridY + "y" + sceneName;
                if (!self.TileDetailsMap.ContainsKey(key))
                {
                    self.TileDetailsMap.Add(key, tile);
                }
            }
        }

        public static void SaveMapData(this GridMapManageComponent self)
        {
            if (self.SavedMapData == null) self.SavedMapData = new SavedMapData();
            else
            {
                self.SavedMapData.TileDetailsList.Clear();
            }
            var sceneName = self.GetParent<Scene>().Name;
            var path = Path.Combine(PathHelper.SavingPath, $"{sceneName}_MapData.sav");

            foreach (var tile in self.TileDetailsMap.Values)
            {
                self.SavedMapData.TileDetailsList.Add(tile);
            }

            ProtobufHelper.SaveTo(self.SavedMapData, path);
        }

        public static void RefreshMap(this GridMapManageComponent self)
        {
            if (self.DigTilemap != null)
            {
                self.DigTilemap.ClearAllTiles();
            }

            if (self.WaterTilemap != null)
            {
                self.WaterTilemap.ClearAllTiles();
            }
            
            self.DisplayMap();
        }
        
        public static void DisplayMap(this GridMapManageComponent self)
        {
            foreach (var tile in self.SavedMapData.TileDetailsList)
            {
                if (tile.DaysSinceDug != -1)
                {
                    self.SetDigTile(tile);
                }

                if (tile.DaysSinceWatered != -1)
                {
                    self.SetWaterTile(tile);
                }
                //TODO:种子
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

        public static void UpdateTileWithDayChange(this GridMapManageComponent self, GameTimeComponent time, int updateTime, bool needRefreshMap = true)
        {
            foreach (var tile in self.TileDetailsMap.Values)
            {
                if (tile.DaysSinceWatered > -1)
                {
                    tile.DaysSinceWatered = -1;
                }

                if (tile.DaysSinceDug > -1)
                {
                    tile.DaysSinceDug += updateTime;
                }

                //超期删除挖坑
                if (tile.DaysSinceDug > 5 && tile.SeedItemId == -1)
                {
                    tile.CanDig = true;
                    tile.CanDropItem = true;
                    tile.DaysSinceDug = -1;
                }
            }

            if (needRefreshMap)
                self.RefreshMap();
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