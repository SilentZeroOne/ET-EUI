using ET.EventType;
using UnityEngine;

namespace ET
{
    [FriendClassAttribute(typeof(ET.GridMapManageComponent))]
    public class SceneChangeFinish_SetCurrentGrid : AEventAsync<SceneChangeFinish>
    {
        protected override async ETTask Run(SceneChangeFinish a)
        {
            a.CurrentScene.GetComponent<GridMapManageComponent>().CurrentGrid = UnityEngine.Object.FindObjectOfType<Grid>();
            await ETTask.CompletedTask;
        }
    }
}