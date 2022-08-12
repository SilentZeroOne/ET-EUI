﻿using System.IO;

namespace ET
{
    public class InventoryComponentAwakeSystem1: AwakeSystem<InventoryComponent,string>
    {
        public override void Awake(InventoryComponent self, string a)
        {
            self.SavePath = a;
            self.LoadInventory().Coroutine();
        }
    }
    
    public class InventoryComponentAwakeSystem: AwakeSystem<InventoryComponent>
    {
        public override void Awake(InventoryComponent self)
        {
#if NOT_UNITY
            return;
#else
            self.SavePath = PathHelper.InventorySavePath;
            self.LoadInventory().Coroutine();
#endif
        }
    }

    public class InventoryComponentDestroySystem: DestroySystem<InventoryComponent>
    {
        public override void Destroy(InventoryComponent self)
        {
            foreach (var item in self.ItemDict.Values)
            {
                item?.Dispose();
            }
            
            self.ItemDict.Clear();
            self.ItemMap.Clear();
            self.ItemConfigIdMap.Clear();
            self.ItemConfigIdList.Clear();
            self.IndexConfigIdDict.Clear();
        }
    }
    
    [FriendClassAttribute(typeof(ET.Item))]
    public class InventoryComponentDeserializeSystem : DeserializeSystem<InventoryComponent>
    {
        public override void Deserialize(InventoryComponent self)
        {
            foreach (var entity in self.Children.Values)
            {
                self.AddContainer(entity as Item);
                Log.Debug($"{(entity as Item).ConfigId}");
            }
        }
    }

    [FriendClass(typeof(InventoryComponent))]
    [FriendClassAttribute(typeof(ET.Item))]
    public static class InventoryComponentSystem
    {
        public static bool IsMaxCapacity(this InventoryComponent self)
        {
            NumericComponent numericComponent = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene()).GetComponent<NumericComponent>();
            return self.ItemConfigIdList.Count >= numericComponent.GetAsInt(NumericType.InventoryCapacity);
        }

        public static bool AddItem(this InventoryComponent self, Item item)
        {
            if (item == null || item.IsDisposed)
            {
                Log.Debug("Item 不存在");
                return false;
            }

            if (self.IsMaxCapacity())
            {
                Log.Debug("背包已满");
                return false;
            }

            if (!self.AddContainer(item))
            {
                Log.Debug("Item 添加失败");
                return false;
            }

            if (item.Parent != self)
            {
                self.AddChild(item);
            }

            return true;
        }

        public static void AddItemByIndex(this InventoryComponent self, Item item, int index)
        {
            if (self.AddItem(item))
            {
                self.IndexConfigIdDict.Add(index, item.ConfigId);
            }
        }

        /// <summary>
        /// 添加进Inventory容器
        /// </summary>
        /// <param name="self"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool AddContainer(this InventoryComponent self, Item item)
        {
            if (self.ItemDict.ContainsKey(item.Id))
            {
                return false;
            }

            self.ItemDict.Add(item.Id, item);
            self.ItemMap.Add(item.Config.ItemType, item);
            self.ItemConfigIdMap.Add(item.ConfigId, item);
            if (!self.ItemConfigIdList.Contains(item.ConfigId))
                self.ItemConfigIdList.Add(item.ConfigId);
            return true;
        }

        public static void RemoveItem(this InventoryComponent self, Item item,bool dispose = true)
        {
            self.RemoveContainer(item);
            if (dispose)
                item.Dispose();
        }

        public static void RemoveContainer(this InventoryComponent self, Item item)
        {
            self.ItemDict.Remove(item.Id);
            self.ItemMap.Remove(item.Config.ItemType, item);
            self.ItemConfigIdMap.Remove(item.ConfigId, item);
            if (self.ItemConfigIdMap.TryGetValue(item.ConfigId, out var list))
            {
                if (list.Count <= 0)
                {
                    self.ItemConfigIdList.Remove(item.ConfigId);
                }
            }

            self.IndexConfigIdDict.RemoveByValue(item.ConfigId);
        }

        public static Item GetItemById(this InventoryComponent self, long id)
        {
            self.ItemDict.TryGetValue(id, out var item);
            return item;
        }
        
        public static Item GetItemByConfigId(this InventoryComponent self, int configId)
        {
            self.ItemConfigIdMap.TryGetValue(configId, out var item);
            return item?[0];
        }

        public static int GetItemCountByConfigId(this InventoryComponent self, int configId)
        {
            if (self.ItemConfigIdMap.TryGetValue(configId, out var list))
            {
                return list.Count;
            }

            return 0;
        }

        public static Item GetItemByIndex(this InventoryComponent self, int index)
        {
            return self.GetItemByConfigId(self.IndexConfigIdDict.GetValueByKey(index));
        }

        #region Save/Load

        public static InventoryProto ToProto(this InventoryComponent self)
        {
            InventoryProto proto = new InventoryProto();
            foreach (var item in self.ItemDict.Values)
            {
                ItemInfo info = item.ToProto();
                info.IndexInInventory = self.IndexConfigIdDict.GetKeyByValue(item.ConfigId);
                proto.ItemInfos.Add(info);
            }

            return proto;
        }

        public static void FromProto(this InventoryComponent self, InventoryProto proto)
        {
            foreach (var item in proto.ItemInfos)
            {
                Item temp = self.AddChild<Item>();
                temp.FromProto(item);

                self.AddItemByIndex(temp, item.IndexInInventory);
                
                //self.AddContainer(temp.FromProto(item));
                Log.Debug($"Add item {temp.Id} {temp.Config.ItemName} to Inventory form memory");
            }
        }

        public static void SaveInventory(this InventoryComponent self, string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
#if NOT_UNITY
                path = "";
#else
                path = PathHelper.InventorySavePath;
#endif
                Log.Debug($"Save inventory in {path}");
            }

            ProtobufHelper.SaveTo(self.ToProto(), path);
        }

        public static async ETTask LoadInventory(this InventoryComponent self)
        {
#if NOT_UNITY
            return;
#else
            string path = self.SavePath;
            
            byte[] bytes = await FileReadHelper.DownloadData(path);
            InventoryProto proto = ProtobufHelper.Deserialize<InventoryProto>(bytes);
            if (proto != null)
                self.FromProto(proto);
#endif
        }

        #endregion
        
    }
}