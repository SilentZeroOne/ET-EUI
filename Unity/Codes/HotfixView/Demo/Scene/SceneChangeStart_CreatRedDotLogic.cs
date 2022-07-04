using ET.EventType;

namespace ET
{
    public class SceneChangeStart_CreatRedDotLogic : AEvent<SceneChangeStart>
    {
        protected override void Run(SceneChangeStart a)
        {
            RedDotHelper.AddRedDotNode(a.ZoneScene, "Root", "Main", false);
            RedDotHelper.AddRedDotNode(a.ZoneScene, "Main", "Role", false);
            RedDotHelper.AddRedDotNode(a.ZoneScene, "Role", "UpLevelButton", false);
            RedDotHelper.AddRedDotNode(a.ZoneScene, "Role", "AddAttribute", false);
        }
    }
}