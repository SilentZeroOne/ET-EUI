using System;

namespace ET
{
    public class C2M_SellBagItemHandler : AMActorLocationRpcHandler<Unit,C2M_SellItem,M2C_SellItem>
    {
        protected override async ETTask Run(Unit unit, C2M_SellItem request, M2C_SellItem response, Action reply)
        {
            BagComponent bagComponent = unit.GetComponent<BagComponent>();

            if (!bagComponent.IsItemExit(request.ItemUid))
            {
                response.Error = ErrorCode.ERR_ItemNotExist;
                reply();
                return;
            }

            Item item = bagComponent.GetItemById(request.ItemUid);
            int addGold = item.Config.SellBasePrice;
            bagComponent.RemoveItem(item);
            unit.GetComponent<NumericComponent>()[NumericType.Gold] += addGold;

            reply();
            await ETTask.CompletedTask;
        }
    }
}