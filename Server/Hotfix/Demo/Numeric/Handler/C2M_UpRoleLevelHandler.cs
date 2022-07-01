using System;

namespace ET
{
    public class C2M_UpRoleLevelHandler : AMActorLocationRpcHandler<Unit,C2M_UpRoleLevel,M2C_UpRoleLevel>
    {
        protected override async ETTask Run(Unit unit, C2M_UpRoleLevel request, M2C_UpRoleLevel response, Action reply)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int level = numericComponent.GetAsInt(NumericType.Level);
            long exp = numericComponent.GetAsLong(NumericType.Exp);
            PlayerLevelConfig config = PlayerLevelConfigCategory.Instance.Get(level);

            if (exp < config.NeedExp)
            {
                response.Error = ErrorCode.ERR_ExpNotEnough;
                reply();
                return;
            }

            var newExp = exp - config.NeedExp;
            if (newExp < 0)
            {
                response.Error = ErrorCode.ERR_ExpNumError;
                reply();
                return;
            }

            numericComponent[NumericType.Level] = level + 1;
            numericComponent[NumericType.Exp] = newExp;
            numericComponent[NumericType.AttributePoint] += 1;
            
            reply();
            await ETTask.CompletedTask;
        }
    }
}