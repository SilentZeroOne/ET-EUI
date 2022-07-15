using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_StartProductionHandler: AMActorLocationRpcHandler<Unit, C2M_StartProduction, M2C_StartProduction>
    {
        protected override async ETTask Run(Unit unit, C2M_StartProduction request, M2C_StartProduction response, Action reply)
        {
            if (!ForgeProductionConfigCategory.Instance.Contain(request.ConfigId))
            {
                response.Error = ErrorCode.ERR_MakeConfigNotExist;
                reply();
                return;
            }

            ForgeComponent forgeComponent = unit.GetComponent<ForgeComponent>();

            if (!forgeComponent.IsExistFreeQueue())
            {
                response.Error = ErrorCode.ERR_NoMakeFreeQueue;
                reply();
                return;
            }
            
            //判断生产材料是否满足
            var config = ForgeProductionConfigCategory.Instance.Get(request.ConfigId);
            var numericComponent = unit.GetComponent<NumericComponent>();
            List<int> materialCounts = new List<int>();
            for (int i = 0; i < config.ConsumIds.Length; i++)
            {
                materialCounts.Add(numericComponent.GetAsInt(config.ConsumIds[i]));
            }
            
            bool enableToMake = true;
            for (int i = 0; i < config.ConsumCounts.Length; i++)
            {
                enableToMake &= materialCounts[i] > config.ConsumCounts[i];
            }

            if (!enableToMake)
            {
                response.Error = ErrorCode.ERR_ConsumeNotEnough;
                reply();
                return;
            }

            for (int i = 0; i < config.ConsumIds.Length; i++)
            {
                numericComponent[config.ConsumIds[i]] -= config.ConsumCounts[i];
            }

            Production production = forgeComponent.StartProduction(request.ConfigId);
            response.ProductionProto = production.ToMessage();

            reply();
            await ETTask.CompletedTask;
        }
    }
}