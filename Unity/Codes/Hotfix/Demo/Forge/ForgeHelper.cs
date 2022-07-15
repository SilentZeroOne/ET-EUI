using System;
using System.Collections.Generic;

namespace ET
{
    public static class ForgeHelper
    {
        public static async ETTask<int> StartProduction(Scene zoneScene, int productionId)
        {
            //判断生成配方是否存在
            if (ForgeProductionConfigCategory.Instance.Contain(productionId))
            {
                return ErrorCode.ERR_ProductionNotExist;
            }
            
            //判断生产材料是否满足
            var config = ForgeProductionConfigCategory.Instance.Get(productionId);
            var numericComponent = UnitHelper.GetMyUnitNumericComponent(zoneScene.CurrentScene());
            List<int> materialCounts = new List<int>();
            for (int i = 0; i < config.ConsumIds.Length; i++)
            {
                materialCounts.Add(numericComponent.GetAsInt(config.ConsumIds[i]));
            }
            Convert.ToInt32()
            bool enableToMake = true;
            for (int i = 0; i < config.ConsumCounts.Length; i++)
            {
                enableToMake &= materialCounts[i] > config.ConsumCounts[i];
            }

            if (!enableToMake) return ErrorCode.ERR_ConsumeNotEnough;

            M2C_StartProduction m2CStartProduction = null;
            try
            {
                m2CStartProduction = (M2C_StartProduction)await zoneScene.GetComponent<SessionComponent>().Session
                        .Call(new C2M_StartProduction() { ConfigId = productionId });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            if (m2CStartProduction.Error != ErrorCode.ERR_Success)
            {
                return m2CStartProduction.Error;
            }

            zoneScene.GetComponent<ForgeComponent>().AddOrUpdateProductionQueue(m2CStartProduction.ProductionProto);
            
            return ErrorCode.ERR_Success;
        }
    }
}