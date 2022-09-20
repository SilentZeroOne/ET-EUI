using UnityEngine;

namespace ET
{
    public class CropViewComponentAwakeSystem: AwakeSystem<CropViewComponent>
    {
        public override void Awake(CropViewComponent self)
        {

        }
    }

    public class CropViewComponentDestroySystem: DestroySystem<CropViewComponent>
    {
        public override void Destroy(CropViewComponent self)
        {

        }
    }

    [FriendClass(typeof(CropViewComponent))]
    [FriendClassAttribute(typeof(ET.GridTile))]
    public static class CropViewComponentSystem
    {
        public static async ETTask Init(this CropViewComponent self)
        {
            Crop crop = self.GetParent<Crop>();
            var go = crop.GetComponent<GameObjectComponent>().GameObject;
            go.AddComponent<MonoBridge>().BelongToEntityId = crop.InstanceId;

            self.SpriteRenderer = go.GetComponentFormRC<SpriteRenderer>("Sprite");
            self.BoxCollider2D = go.GetComponent<BoxCollider2D>();
        }

        public static int GetCurrentStage(this CropViewComponent self)
        {
            var crop = self.GetParent<Crop>();
            var tile = crop.GetParent<GridTile>();

            var growthDays = crop.Config.GrowthDays.Length - 1;
            var dayCount = crop.Config.TotalGrowthDays;

            var currentStage = 0;
            for (int i = growthDays; i >= 0; i--)
            {
                if (tile.GrowthDays >= dayCount)
                {
                    currentStage = i;
                }

                dayCount -= crop.Config.GrowthDays[i];
            }

            return currentStage;
        }
    }
}