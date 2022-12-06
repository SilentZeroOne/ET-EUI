using System;

namespace ET
{
    public class G2M_RequestExitGameHandler: AMActorLocationRpcHandler<Unit, G2M_RequestExitGame, M2G_RequestExitGame>
    {
        protected override async ETTask Run(Unit unit, G2M_RequestExitGame request, M2G_RequestExitGame response, Action reply)
        {
            Log.Debug("开始下线保存玩家数据");
            unit.GetComponent<UnitDBSaveComponent>()?.SaveChange();
            
            reply();

            await unit.RemoveLocation();
            UnitComponent unitComponent = unit.DomainScene().GetComponent<UnitComponent>();
            unitComponent?.Remove(unit.Id);
        }
    }
}