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

    [FriendClass(typeof(ItemPickerComponent))]
    [FriendClassAttribute(typeof(ET.InventoryComponent))]
    public static class ItemPickerComponentSystem
    {
        public static void PickUpItem(this ItemPickerComponent self, Collider2D other)
        {
            if (other.CompareTag(TagManager.Item))
            {
                var bridge = other.GetComponent<MonoBridge>();
                if (bridge)
                {
                    Item item = Game.EventSystem.Get(bridge.BelongToEntityId) as Item;
                    if (item.Config.CanPickUp == 1)
                    {
                        var errorCode = ItemHelper.AddItemFromCurrentScene(item);
                        if (errorCode == ErrorCode.ERR_Success)
                        {
                            UnityEngine.Object.Destroy(other.gameObject);
                        }
                    }
                }
            }
        }
    }
}