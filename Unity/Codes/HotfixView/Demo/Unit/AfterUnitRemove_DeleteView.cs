using ET.EventType;

namespace ET
{
    [FriendClassAttribute(typeof(ET.LandRoomObjectsComponent))]
    public class AfterUnitRemove_DeleteView : AEvent<AfterUnitRemove>
    {
        protected override void Run(AfterUnitRemove a)
        {
            var currentScene = a.Unit.ZoneScene().CurrentScene();
            LandRoomObjectsComponent landRoomObjectsComponent = currentScene.GetComponent<LandRoomObjectsComponent>();
            var index = landRoomObjectsComponent.Seats[a.Unit.Id];
            landRoomObjectsComponent.Seats.Remove(a.Unit.Id);

            var go = a.Unit.GetComponent<GameObjectComponent>().GameObject;
            UnityEngine.Object.Destroy(go);
        }
    }
}