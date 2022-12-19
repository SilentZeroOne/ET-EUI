namespace ET
{
    
    public class SceneChangeFinish_ShowCurrentSceneUI: AEventAsync<EventType.SceneChangeFinish>
    {
        protected override async ETTask Run(EventType.SceneChangeFinish args)
        {
            //args.ZoneScene.GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Lobby);
            if (args.CurrentScene.Name == "PlayingRoom")
            {
                args.ZoneScene.GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Match);
                args.ZoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Main);
            }
            
            await ETTask.CompletedTask;
        }
    }
    
}