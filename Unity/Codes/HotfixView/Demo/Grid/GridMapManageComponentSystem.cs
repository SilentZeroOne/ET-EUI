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


    [FriendClass(typeof(GridMapManageComponent))]
    [FriendClassAttribute(typeof(ET.GridTile))]
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

        /// <summary>
        /// 填充初始数据
        /// </summary>
        private static void FillTileDetailsMap(this GridMapManageComponent self, MapData data)
        {
            var sceneName = self.GetParent<Scene>().Name;
            foreach (var tile in data.Tiles)
            {
                var key = tile.TileCoordinate.X + "x" + tile.TileCoordinate.Y + "y" + sceneName;
                if (!self.GridTilesMap.ContainsKey(key))
                {
                    var gridTile = self.AddChild<GridTile, int, int>(tile.TileCoordinate.X, tile.TileCoordinate.Y);
                    self.GridTilesMap.Add(key, gridTile);
                }

                switch ((GridType)tile.GridType)
                {
                    case GridType.Digable:
                        self.GridTilesMap[key].CanDig = tile.Value;
                        break;
                    case GridType.DropItem:
                        self.GridTilesMap[key].CanDropItem = tile.Value;
                        break;
                    case GridType.PlaceFurniture:
                        self.GridTilesMap[key].CanPlaceFurniture = tile.Value;
                        break;
                    case GridType.NPCObstacle:
                        self.GridTilesMap[key].IsNPCObstacle = tile.Value;
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
                if (!self.GridTilesMap.ContainsKey(key))
                {
                    var gridTile = self.AddChild<GridTile>();
                    gridTile.FromProto(tile);
                    self.GridTilesMap.Add(key, gridTile);
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

            foreach (var tile in self.GridTilesMap.Values)
            {
                self.SavedMapData.TileDetailsList.Add(tile.ToProto());
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
            foreach (var tile in self.GridTilesMap.Values)
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
        public static GridTile GetGridTile(this GridMapManageComponent self, string key)
        {
            self.GridTilesMap.TryGetValue(key, out var gridTile);
            return gridTile;
        }

        public static GridTile GetGridTile(this GridMapManageComponent self, int x, int y)
        {
            var currentName = self.GetParent<Scene>().Name;
            var key = $"{x}x{y}y{currentName}";
            return self.GetGridTile(key);
        }

        public static void UpdateTileWithDayChange(this GridMapManageComponent self, GameTimeComponent time, int updateTime, bool needRefreshMap = true)
        {
            foreach (var tile in self.GridTilesMap.Values)
            {
                if (tile.DaysSinceWatered > -1 && updateTime > 0)
                {
                    tile.DaysSinceWatered = -1;
                }

                if (tile.DaysSinceDug > -1)
                {
                    tile.DaysSinceDug += updateTime;
                }

                //超期删除挖坑
                if (tile.DaysSinceDug > 5 && tile.Crop == null)
                {
                    tile.CanDig = true;
                    tile.CanDropItem = true;
                    tile.DaysSinceDug = -1;
                    tile.GrowthDays = -1;
                }

                if (tile.Crop != null)
                {
                    tile.GrowthDays += updateTime;
                    tile.Crop.GetComponent<CropViewComponent>().Init().Coroutine();
                }
            }

            if (needRefreshMap)
                self.RefreshMap();
        }

        public static void SetDigTile(this GridMapManageComponent self, GridTile gridTile)
        {
            if (self.DigTilemap != null)
            {
                var pos = new Vector3Int(gridTile.GridX, gridTile.GridY, 0);
                self.DigTilemap.SetTile(pos, self.DigTile);
            }
        }

        public static void SetWaterTile(this GridMapManageComponent self, GridTile gridTile)
        {
            if (self.WaterTilemap != null)
            {
                var pos = new Vector3Int(gridTile.GridX, gridTile.GridY, 0);
                self.WaterTilemap.SetTile(pos, self.WaterTile);
            }
        }
    }
}