using ET.EventType;

namespace ET
{
    public class AfterCropCreate_CreateCropView : AEvent<AfterCropCreate>
    {
        protected override void Run(AfterCropCreate a)
        {
            if (a.Crop.GetComponent<GameObjectComponent>() == null)
            {
                a.Crop.AddComponent<GameObjectComponent>();
            }
            
            if (a.Crop.GetComponent<CropViewComponent>() == null)
            {
                a.Crop.AddComponent<CropViewComponent>();
            }
            
            a.Crop.GetComponent<CropViewComponent>().Init(a.ForceDisplay).Coroutine();
        }
    }
}