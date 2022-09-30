using System;
using System.IO;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

namespace ET
{
    [ExecuteInEditMode]
    public class GridMap : MonoBehaviour
    {
        public string SceneName;
        public Vector2Int GridSize;
        public Vector2Int StartNode;
        
        private MapData MapData;
        public GridType Type;
        private Tilemap _currentTilemap;
        private string _savePath;

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
                        this.MapData.GridSize = new ProtoVector2Int(this.GridSize.x, this.GridSize.y);
                        this.MapData.StartNode = new ProtoVector2Int(this.StartNode.x, this.StartNode.y);
                    }
                }

                if (this._currentTilemap == null)
                {
                    this._currentTilemap = this.GetComponent<Tilemap>();
                }
            }
        }

        private void OnDisable()
        {
            if (!Application.IsPlaying(this))
            {
                if (this._currentTilemap == null)
                {
                    this._currentTilemap = this.GetComponent<Tilemap>();
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
            if (this._currentTilemap == null) return;
            
            this._currentTilemap.CompressBounds();

            if (!Application.IsPlaying(this))
            {
                //Proto 类貌似在Editor中 一直是null 但可以正常使用
                // if (this.MapData != null)
                // {
                    var cellBounds = this._currentTilemap.cellBounds;
                    Vector3Int startPos = cellBounds.min;
                    Vector3Int endPos = cellBounds.max;

                    for (int x = startPos.x; x < endPos.x; x++)
                    {
                        for (int y = startPos.y; y < endPos.y; y++)
                        {
                            TileBase tile = this._currentTilemap.GetTile(new Vector3Int(x, y, 0));
                            if (tile != null)
                            {
                                TileProperty newTile = new TileProperty()
                                {
                                    TileCoordinate = new ProtoVector2Int(x, y), GridType = (int)this.Type, Value = true
                                };
                                
                                this.MapData.Tiles.Add(newTile);
                            }
                        }
                    }
                //}
            }
        }
    }
}