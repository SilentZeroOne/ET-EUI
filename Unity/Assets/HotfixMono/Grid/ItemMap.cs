using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ET
{
    [ExecuteInEditMode]
    public class ItemMap : MonoBehaviour
    {
        public string SceneName;
        private MapData MapData;
        public GridType Type;
        private Grid _currentGrid;
        private string _savePath;
        public List<SceneItem> SceneObjs = new List<SceneItem>();

        private async void OnEnable()
        {
            if (!Application.IsPlaying(this))
            {
                this._savePath = Path.Combine(PathHelper.BundlePath, $"MapData/{this.SceneName}_{this.Type}_data.bytes");

                if (this.MapData == null)
                {
                    var data = await FileReadHelper.DownloadData(this._savePath);
                    if (data != null)
                    {
                        this.MapData = MonoPbHelper.Deserialize<MapData>(data);
                        this.MapData.Tiles.Clear();
                    }
                    else
                    {
                        this.MapData = new MapData();
                    }
                }

                if (this._currentGrid == null)
                {
                    this._currentGrid = FindObjectOfType<Grid>();
                }

                this.SceneObjs.Clear();
                var sceneItems = this.GetComponentsInChildren<SceneItem>();
                this.SceneObjs.AddRange(sceneItems);
            }
        }

        private void OnDisable()
        {
            if (!Application.IsPlaying(this))
            {
                if (this._currentGrid == null)
                {
                    this._currentGrid = FindObjectOfType<Grid>();
                }
                
                this.UpdateTileProperties();

                Debug.Log($"Save map data in {this._savePath}");
                MonoPbHelper.SaveTo(this.MapData, this._savePath);
#if UNITY_EDITOR
                AssetDatabase.Refresh();
#endif
            }
        }

        private void UpdateTileProperties()
        {
            if (this._currentGrid == null) return;

            if (!Application.IsPlaying(this))
            {
                foreach (var obj in this.SceneObjs)
                {
                    Vector3Int pos = this._currentGrid.WorldToCell(obj.transform.position);
                    TileProperty newTile = new TileProperty()
                    {
                        TileCoordinate = new ProtoVector2Int(pos.x, pos.y),
                        GridType = (int)this.Type,
                        Value = true,
                        Crop = new MonoCropInfo() { ConfigId = obj.ConfigId, LastStage = obj.LastStage, IsItem = obj.IsItem },
                        GrowthDays = obj.GrowthDays,
                        DaysSinceDug = obj.DaysSinceDug
                    };
                    
                    this.MapData.Tiles.Add(newTile);
                }
            }
        }
    }
}