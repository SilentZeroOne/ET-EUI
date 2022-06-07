using ET.EventType;

namespace ET
{
    public class AdventureBattleReportEvent_RequestEndGameLevel : AEventAsync<AdventureBattleReport>
    {
        protected override async ETTask Run(AdventureBattleReport a)
        {
            if (a.BattleRoundResult == BattleRoundResult.KeepBattle)
            {
                return;
            }

            int errorCode = await AdventureHelper.RequestEndGameLevel(a.ZoneScene, a.BattleRoundResult, a.Round);

            if (errorCode != ErrorCode.ERR_Success)
            {
                return;
            }

            await TimerComponent.Instance.WaitAsync(3000);
            
            a.ZoneScene?.CurrentScene()?.GetComponent<AdventureComponent>()?.ResetAdventure();
        }
    }
}