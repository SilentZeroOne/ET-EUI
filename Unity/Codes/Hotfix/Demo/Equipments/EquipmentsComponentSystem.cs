namespace ET
{
    public class EquipmentsComponentDestroySystem: DestroySystem<EquipmentsComponent>
    {
        public override void Destroy(EquipmentsComponent self)
        {
            self.Clear();
        }
    }
    
    [FriendClass(typeof(EquipmentsComponent))]
    public static class EquipmentsComponentSystem
    {
        public static Item GetEquipItemByPosition(this EquipmentsComponent self, EquipPosition position)
        {
            self.EquipItems.TryGetValue((int)position, out var item);
            return item;
        }
        
        public static bool EquipItem(this EquipmentsComponent self, Item item)
        {
            if (!self.EquipItems.ContainsKey(item.Config.EquipPosition))
            {
                self.AddChild(item);
                self.EquipItems.Add(item.Config.EquipPosition, item);
                return true;
            }

            return false;
        }

        public static bool UnEquipItem(this EquipmentsComponent self, Item item)
        {
            if (self.EquipItems.ContainsKey(item.Config.EquipPosition))
            {
                self.EquipItems.Remove(item.Config.EquipPosition);
                item?.Dispose();
                return true;
            }

            return false;
        }

        public static Item GetEquipItemByID(this EquipmentsComponent self, long itemId)
        {
            if (self.Children.TryGetValue(itemId, out var item))
            {
                return item as Item;
            }

            return null;
        }

        public static void Clear(this EquipmentsComponent self)
        {
            ForeachHelper.Foreach(self.EquipItems,((i, item) =>
            {
                item?.Dispose();
            } ));
            
            self.EquipItems.Clear();
        }
    }
}