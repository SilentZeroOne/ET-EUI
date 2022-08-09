using UnityEngine;

namespace ET
{
    public class ItemPickerComponentAwakeSystem: AwakeSystem<ItemPickerComponent>
    {
        public override void Awake(ItemPickerComponent self)
        {
            var go = self.GetParent<Unit>().GetComponent<GameObjectComponent>().GameObject;
            self.TriggerAction = go.GetComponent<TriggerAction>();
            if (self.TriggerAction == null)
            {
                self.TriggerAction = go.AddComponent<TriggerAction>();
            }
            
            self.TriggerAction.OnTriggerEnter2DAction += self.PickUpItem;
        }
    }

    public class ItemPickerComponentDestroySystem: DestroySystem<ItemPickerComponent>
    {
        public override void Destroy(ItemPickerComponent self)
        {

        }
    }

    [FriendClass(typeof (ItemPickerComponent))]
    public static class ItemPickerComponentSystem
    {
        public static void PickUpItem(this ItemPickerComponent self, Collider2D other)
        {
            if (other.CompareTag(TagManager.Item))
            {
                var bridge = other.GetComponent<MonoBridge>();
                if (bridge)
                {
                    Unit player = self.GetParent<Unit>();
                    Item item = Game.EventSystem.Get(bridge.BelongToEntityId) as Item;
                    if (item.Config.CanPickUp == 1)
                    {
                        player.GetComponent<InventoryComponent>().AddItem(item);
                        UnityEngine.Object.Destroy(other.gameObject);
                    }
                }
            }
        }
    }
}