namespace ET
{
    public static class ItemHelper
    {
        public static int AddItemFromCurrentScene(Item item)
        {
            Unit player = UnitHelper.GetMyUnitFromCurrentScene(item.ZoneScene().CurrentScene());
            player.ZoneScene().CurrentScene().GetComponent<ItemsComponent>().RemoveItem(item);
            //捡到物品现在直接加入快捷栏
            InventoryComponent actionBar = player.ZoneScene().GetComponent<InventoryComponent>();
            int errorCode = actionBar.AddItem(item);
            if (errorCode == ErrorCode.ERR_BagOverCapacity)//快捷栏位超过后 加入背包
            {
                InventoryComponent inventoryComponent = player.GetComponent<InventoryComponent>();
                errorCode = inventoryComponent.AddItem(item);
            }
                        
            if (errorCode == ErrorCode.ERR_Success)
            {
                Game.EventSystem.Publish(new EventType.RefreshInventory() { ZoneScene = item.ZoneScene() });
            }
            else
            {
                Log.Debug(errorCode.ToString());
            }

            return errorCode;
        }
    }
}