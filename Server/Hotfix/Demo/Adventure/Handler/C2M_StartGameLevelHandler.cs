using System;

namespace ET
{
    public class C2M_StartGameLevelHandler : AMActorLocationRpcHandler<Unit,C2M_StartGameLevel,M2C_StartGameLevel>
    {
        protected override async ETTask Run(Unit unit, C2M_StartGameLevel request, M2C_StartGameLevel response, Action reply)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

            if (numericComponent.GetAsInt(NumericType.AdventureState) != 0)
            {
                response.Error = ErrorCode.ERR_AlreadyInAdventure;
                reply();
                return;
            }

            if (numericComponent.GetAsInt(NumericType.DyingState) != 0)
            {
                response.Error = ErrorCode.ERR_AdventureInDying;
                reply();
                return;
            }

            if (!BattleLevelConfigCategory.Instance.Contain(request.LevelId))
            {
                response.Error = ErrorCode.ERR_AdventureErrorLevel;
                reply();
                return;
            }

            BattleLevelConfig config = BattleLevelConfigCategory.Instance.Get(request.LevelId);
            if (numericComponent[NumericType.Level] < config.MinEnterLevel[0])
            {
                response.Error = ErrorCode.ERR_AdventureLevelNotEnough;
                reply();
                return;
            }

            numericComponent.Set(NumericType.AdventureState, request.LevelId);
            numericComponent.Set(NumericType.AdventureStartTime,TimeHelper.ServerNow());
            reply();
            
            await ETTask.CompletedTask;
        }
    }
}