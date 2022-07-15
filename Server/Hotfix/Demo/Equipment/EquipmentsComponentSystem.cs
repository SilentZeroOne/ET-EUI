using ET.EventType;

namespace ET
{
    public class EquipmentsComponentDestroySystem: DestroySystem<EquipmentsComponent>
    {
        public override void Destroy(EquipmentsComponent self)
        {
            foreach (var item in self.EquipItems.Values)
            {
                item?.Dispose();
            }
            self.EquipItems.Clear();
            self.message = null;
        }
    }

    public class EquipmentsComponentDeserializeSystem: DeserializeSystem<EquipmentsComponent>
    {
        public override void Deserialize(EquipmentsComponent self)
        {
            foreach (var entity in self.Children.Values)
            {
                self.AddContainer(entity as Item);
            }
        }
    }
    
    [FriendClass(typeof(EquipmentsComponent))]
    public static class EquipmentsComponentSystem
    {
        public static bool EquipItem(this EquipmentsComponent self, Item item)
        {
            if (!self.EquipItems.ContainsKey(item.Config.EquipPosition))
            {
                self.AddChild(item);
                self.EquipItems.Add(item.Config.EquipPosition, item);
                Game.EventSystem.Publish(new ChangeEquipItem(){Unit = self.GetParent<Unit>(), EquipOp = EquipOp.Load, Item = item});
                ItemUpdateNoticeHelper.SyncAddItem(self.GetParent<Unit>(), item, self.message);
                return true;
            }

            return false;
        } 
        
        public static Item UnLoadEquipItemByPosition(this EquipmentsComponent self, int position)
        {
            if (self.EquipItems.TryGetValue(position, out var item))
            {
                self.EquipItems.Remove(position);
                Game.EventSystem.Publish(new ChangeEquipItem() { Unit = self.GetParent<Unit>(), EquipOp = EquipOp.UnLoad, Item = item });
                ItemUpdateNoticeHelper.SyncRemoveItem(self.GetParent<Unit>(), item, self.message);
            }

            return item;
        }
        
        public static Item GetEquipItemByPosition(this EquipmentsComponent self, int position)
        {
            self.EquipItems.TryGetValue(position, out var item);
            return item;
        }

        public static bool AddContainer(this EquipmentsComponent self, Item item)
        {
            if (self.EquipItems.ContainsKey(item.Config.EquipPosition))
            {
                return false;
            }

            self.EquipItems.Add(item.Config.EquipPosition, item);
            return true;
        }

        public static bool IsEquipItemByPosition(this EquipmentsComponent self, int equipPosition)
        {
            self.EquipItems.TryGetValue(equipPosition, out var item);
            return item != null && !item.IsDisposed;
        }
        
    }
}