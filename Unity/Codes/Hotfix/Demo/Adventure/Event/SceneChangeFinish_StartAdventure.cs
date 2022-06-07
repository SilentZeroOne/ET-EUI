using ET.EventType;

namespace ET
{
    public class SceneChangeFinish_StartAdventure: AEventAsync<SceneChangeFinish>
    {
        protected override async ETTask Run(SceneChangeFinish a)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(a.CurrentScene);
            if (unit.GetComponent<NumericComponent>()?.GetAsInt(NumericType.AdventureState) == 0)
            {
                return;
            }

            await TimerComponent.Instance.WaitAsync(3000);
            
            a.CurrentScene.GetComponent<AdventureComponent>()?.StartAdventure().Coroutine();
        }
    }
}