using System;

namespace ET
{
    public class C2M_UnLoadEquipItemHandler : AMActorLocationRpcHandler<Unit,C2M_UnloadEquipItem,M2C_UnloadEquipItem>
    {
        protected override async ETTask Run(Unit unit, C2M_UnloadEquipItem request, M2C_UnloadEquipItem response, Action reply)
        {
            EquipmentsComponent equipmentsComponent = unit.GetComponent<EquipmentsComponent>();
            BagComponent bagComponent = unit.GetComponent<BagComponent>();

            if (bagComponent.IsMaxLoad())
            {
                response.Error = ErrorCode.ERR_BagMaxLoad;
                reply();
                return;
            }

            if (!equipmentsComponent.IsEquipItemByPosition(request.EquipPosition))
            {
                response.Error = ErrorCode.ERR_ItemNotExist;
                reply();
                return;
            }

            Item equipItem = equipmentsComponent.GetEquipItemByPosition(request.EquipPosition);

            if (!bagComponent.IsCanAddItem(equipItem))
            {
                response.Error = ErrorCode.ERR_AddBagItemError;
                reply();
                return;
            }

            equipItem = equipmentsComponent.UnLoadEquipItemByPosition(request.EquipPosition);
            bagComponent.AddItem(equipItem);
            
            reply();
            
            await ETTask.CompletedTask;
        }
    }
}