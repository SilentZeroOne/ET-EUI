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
        }
    }

    [FriendClass(typeof (InventoryComponent))]
    public static class InventoryComponentSystem
    {
        public static bool AddItem(this InventoryComponent self, Item item)
        {
            if (item == null || item.IsDisposed)
            {
                Log.Debug("Item 不存在");
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

            return true;
        }

        public static void RemoveContainer(this InventoryComponent self, Item item)
        {
            self.ItemDict.Remove(item.Id);
            self.ItemMap.Remove(item.Config.ItemType, item);
        }
    }
}