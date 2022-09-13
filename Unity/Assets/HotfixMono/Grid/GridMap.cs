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
        private MapData MapData;
        public GridType Type;
        private Tilemap _currentTilemap;
        private string _savePath;

        private async void OnEnable()
        {
            if (!Application.IsPlaying(this))
            {
                if (string.IsNullOrEmpty(this._savePath))
                {
                    this._savePath = Path.Combine(PathHelper.SavingPath, $"{this.SceneName}_{this.Type}_data.sav");
                }
                
                if (this.MapData == null)
                {
                    var data = await FileReadHelper.DownloadData(this._savePath);
                    if (data != null)
                    {
                        this.MapData = Deserialize<MapData>(data);
                        this.MapData.Tiles.Clear();
                    }
                    else
                    {
                        this.MapData = new MapData();
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
                SaveTo(this.MapData, this._savePath);
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

        #region PB

        public static void SaveTo(object entity, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("Save path is Null!");
                return;
            }

            DirectoryInfo df = new DirectoryInfo(path);
            if (!df.Parent.Exists)
            {
                df.Parent.Create();
            }
	        
            if (File.Exists(path))
            {
                File.Delete(path);
            }
	        
            using FileStream stream = File.Create(path);
            Serializer.Serialize(stream, entity);
        }
        
        public static T Deserialize<T>(byte[] bytes) where T : class
        {
            if (bytes == null || bytes.Length == 0)
            {
                //Log.Error("Deserialze data is null!!!");
                return null;
            }

            using MemoryStream stream = new MemoryStream(bytes);
            
            return Serializer.Deserialize<T>(stream);
        }

        #endregion
    }
}