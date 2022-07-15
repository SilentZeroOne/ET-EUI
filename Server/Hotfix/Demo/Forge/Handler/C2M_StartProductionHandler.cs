using System;

namespace ET
{
    public class C2M_StartProductionHandler: AMActorLocationRpcHandler<Unit, C2M_StartProduction, M2C_StartProduction>
    {
        protected override async ETTask Run(Unit unit, C2M_StartProduction request, M2C_StartProduction response, Action reply)
        {
            
            
            reply();
            await ETTask.CompletedTask;
        }
    }
}