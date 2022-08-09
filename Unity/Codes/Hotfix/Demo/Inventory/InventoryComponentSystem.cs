namespace ET
{
    public class InventoryComponentAwakeSystem: AwakeSystem<InventoryComponent>
    {
        public override void Awake(InventoryComponent self)
        {

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
            return self.ItemDict.Count >= numericComponent.GetAsInt(NumericType.InventoryCapacity);
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
            return true;
        }

        public static void RemoveItem(this InventoryComponent self, Item item)
        {
            self.RemoveContainer(item);
            item.Dispose();
        }

        public static void RemoveContainer(this InventoryComponent self, Item item)
        {
            self.ItemDict.Remove(item.Id);
            self.ItemMap.Remove(item.Config.ItemType, item);
            self.ItemConfigIdMap.Remove(item.ConfigId, item);
        }
    }
}