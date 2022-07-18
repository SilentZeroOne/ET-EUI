using System;

namespace ET
{
    public class C2M_EndGameLevelHandler : AMActorLocationRpcHandler<Unit,C2M_EndGameLevel,M2C_EndGameLevel>
    {
        protected override async ETTask Run(Unit unit, C2M_EndGameLevel request, M2C_EndGameLevel response, Action reply)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int level = numericComponent.GetAsInt(NumericType.AdventureState);
            if (level == 0 || !BattleLevelConfigCategory.Instance.Contain(level))
            {
                response.Error = ErrorCode.ERR_AdventureErrorLevel;
                reply();
                return;
            }

            if (request.Round <= 0)
            {
                response.Error = ErrorCode.ERR_AdventureRoundError;
                reply();
                return;
            }

            //战斗失败 进入垂死
            if (request.BattleResult == (int)BattleRoundResult.LoseBattle)
            {
                numericComponent.Set(NumericType.DyingState, 1);
                numericComponent.Set(NumericType.AdventureState, 0);
                reply();
                return;
            }

            if (request.BattleResult != (int)BattleRoundResult.WinBattle)
            {
                response.Error = ErrorCode.ERR_AdventureResultError;
                reply();
                return;
            }

            if (!unit.GetComponent<AdventureCheckComponent>().CheckBattleWinResult(request.Round))
            {
                response.Error = ErrorCode.ERR_AdventureWinResultError;
                reply();
                return;
            }

            var config = BattleLevelConfigCategory.Instance.Get(level);
            numericComponent.Set(NumericType.AdventureState, 0);
            numericComponent[NumericType.Exp] += config.RewardExp;
            
            //添加奖励
            int itemCount = RandomHelper.RandomNumber(3, 7);
            for (int i = 0; i < itemCount; i++)
            {
                int itemConfigId = RandomHelper.RandomNumber(1002, 1019);
                unit.GetComponent<BagComponent>().AddItemByConfig(itemConfigId);
            }
            
            //添加素材(先写死)
            numericComponent[NumericType.IronCount] += 500;
            numericComponent[NumericType.FurCount] += 500;
            
            reply();
            await ETTask.CompletedTask;
        }
    }
}