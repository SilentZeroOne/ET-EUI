using ET.EventType;

namespace ET
{
    [FriendClass(typeof(EquipmentsComponent))]
    public static class EquipmentsComponetSystem
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
        
    }
}