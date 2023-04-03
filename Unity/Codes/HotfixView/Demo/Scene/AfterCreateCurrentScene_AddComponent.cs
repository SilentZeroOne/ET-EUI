namespace ET
{
    public class AfterCreateCurrentScene_AddComponent: AEvent<EventType.AfterCreateCurrentScene>
    {
        protected override void Run(EventType.AfterCreateCurrentScene args)
        {
            Scene currentScene = args.CurrentScene;
            currentScene.AddComponent<UIComponent>();
            if (args.CurrentScene.Name == "Lobby")
            {
                var uiComponent = args.CurrentScene.ZoneScene().GetComponent<UIComponent>();
                uiComponent.HideAllShownWindow();
                uiComponent.ShowWindow(WindowID.WindowID_Match);
            }
        }
    }
}