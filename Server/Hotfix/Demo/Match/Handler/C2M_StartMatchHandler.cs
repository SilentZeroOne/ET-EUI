using System;

namespace ET
{
    public class C2M_StartMatchHandler: AMActorLocationRpcHandler<Unit, C2Lo_StartMatch, Lo2C_StartMatch>
    {
        protected override async ETTask Run(Unit unit, C2Lo_StartMatch request, Lo2C_StartMatch response, Action reply)
        {
            LandMatchComponent landMatchComponent = unit.DomainScene().GetComponent<LandMatchComponent>();
            Log.Info("开始匹配。。。");

            var room = landMatchComponent.GetWaitingRoom(request.RoleInfoId);
            if (room == null)
            {
                landMatchComponent.AddUnitToMatchingQueue(unit);
            }
            
            reply();
            await ETTask.CompletedTask;
        }
    }
}