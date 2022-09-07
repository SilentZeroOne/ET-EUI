using ET.EventType;

namespace ET
{
    public class SceneChangeStart_ShowLoadingUI: AEvent<SceneChangeStart>
    {
        protected override void Run(SceneChangeStart a)
        {
            a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgLoading>().FadeIn();
        }
    }
}