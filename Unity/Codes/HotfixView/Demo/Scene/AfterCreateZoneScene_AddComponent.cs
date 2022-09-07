namespace ET
{
    public class AfterCreateZoneScene_AddComponent: AEvent<EventType.AfterCreateZoneScene>
    {
        protected override  void Run(EventType.AfterCreateZoneScene args)
        {
            Scene zoneScene = args.ZoneScene;
            zoneScene.AddComponent<UIComponent>();
            zoneScene.AddComponent<UIPathComponent>();
            zoneScene.AddComponent<UIEventComponent>();
            zoneScene.AddComponent<RedDotComponent>();
            zoneScene.AddComponent<InventoryComponent, string, InventoryType>(PathHelper.ActionBarSavePath, InventoryType.ActionBar); // For Action Bar.
            //zoneScene.AddComponent<ResourcesLoaderComponent>();
            
            zoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Loading);
            zoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Main);

            var config = GameSceneConfigCategory.Instance.Get(2);

            SceneChangeHelper.SceneChangeTo(zoneScene, config.SceneName, IdGenerater.Instance.GenerateInstanceId()).Coroutine();
        }
    }
}