namespace ET
{
    [FriendClassAttribute(typeof(ET.GridMapManageComponent))]
    public class AfterCreateCurrentScene_AddComponent : AEvent<EventType.AfterCreateCurrentScene>
    {
        protected override void Run(EventType.AfterCreateCurrentScene args)
        {
            Scene currentScene = args.CurrentScene;
            currentScene.AddComponent<UIComponent>();
            RunAsync(args).Coroutine();
        }

        private async ETTask RunAsync(EventType.AfterCreateCurrentScene args)
        {
            var gridMapManager = args.CurrentScene.AddComponent<GridMapManageComponent>();
            await gridMapManager.LoadMapData();
            var astar = args.CurrentScene.AddComponent<AStarComponent>();
            astar.Init(gridMapManager.GridSize.x, gridMapManager.GridSize.y, gridMapManager.StartNode.x, gridMapManager.StartNode.y,
                gridMapManager.GridTilesMap);

        }
    }
}