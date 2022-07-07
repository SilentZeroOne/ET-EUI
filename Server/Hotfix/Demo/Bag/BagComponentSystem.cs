namespace ET
{
    public class BagComponentDeserializeSystem: DeserializeSystem<BagComponent>
    {
        public override void Deserialize(BagComponent self)
        {
            foreach (var entity in self.Children.Values)
            {
                self.AddContainer(entity as Item);
            }
        }
    }


    [FriendClass(typeof(BagComponent))]
    public static class BagComponentSystem
    {
        public static int GetItemCountByItemType(this BagComponent self, ItemType itemType)
        {
            if (self.ItemMap.TryGetValue((int)itemType, out var itemList))
            {
                return itemList.Count;
            }

            return 0;
        }

        public static Item GetItemById(this BagComponent self, long id)
        {
            if (self.ItemsDict.TryGetValue(id, out var item))
            {
                return item;
            }

            return null;
        }

        public static bool IsCanAddItemByConfigId(this BagComponent self, int configId)
        {
            if (!ItemConfigCategory.Instance.Contain(configId))
            {
                return false;
            }

            if (self.IsMaxLoad())
            {
                return false;
            }

            return true;
        }

        public static bool IsMaxLoad(this BagComponent self)
        {
            return self.ItemsDict.Count >= self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt(NumericType.MaxBagCapcity);
        }

        public static bool AddItemByConfig(this BagComponent self, int configId,int count =1)
        {
            if (!ItemConfigCategory.Instance.Contain(configId))
            {
                return false;
            }

            if (count <= 0)
            {
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                Item item = ItemFactory.Create(self, configId);
                if (!self.AddItem(item))
                {
                    Log.Error("添加物品失败！");
                    item?.Dispose();
                    return false;
                }
            }

            return true;
        }

        public static bool AddItem(this BagComponent self, Item item)
        {
            if (item == null || item.IsDisposed)
            {
                Log.Debug("Item is null");
                return false;
            }

            if (self.IsMaxLoad())
            {
                Log.Debug("Bag is max");
                return false;
            }

            if (!self.AddContainer(item))
            {
                Log.Debug("Add container failed");
                return false;
            }

            if (item.Parent != self)
            {
                self.AddChild(item);
            }

            ItemUpdateNoticeHelper.SyncAddItem(self.GetParent<Unit>(), item, self.message);
            return true;
        }

        public static bool AddContainer(this BagComponent self, Item item)
        {
            if (self.ItemsDict.ContainsKey(item.Id))
            {
                return false;
            }

            self.ItemsDict.Add(item.Id, item);
            self.ItemMap.Add(item.Config.Type, item);
            return true;
        }

        public static void RemoveContainer(this BagComponent self, Item item)
        {
            self.ItemsDict.Remove(item.Id);
            self.ItemMap.Remove(item.Config.Type, item);
            ItemUpdateNoticeHelper.SyncRemoveItem(self.GetParent<Unit>(), item, self.message);
        }
    }
}