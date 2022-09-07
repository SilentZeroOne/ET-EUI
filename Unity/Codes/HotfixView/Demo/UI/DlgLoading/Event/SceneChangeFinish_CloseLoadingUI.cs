using ET.EventType;

namespace ET
{
    public class SceneChangeFinish_CloseLoadingUI: AEventAsync<SceneChangeFinish>
    {
        protected override async ETTask Run(SceneChangeFinish a)
        {
            a.ZoneScene.GetComponent<UIComponent>().GetDlgLogic<DlgLoading>().FadeOut();
            await ETTask.CompletedTask;
        }
    }
}