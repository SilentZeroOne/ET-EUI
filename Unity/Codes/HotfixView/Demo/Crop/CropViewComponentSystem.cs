using BM;
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
        public static async ETTask Init(this CropViewComponent self,bool forceDisplay = false)
        {
            Crop crop = self.GetParent<Crop>();
            var tile = crop.GetParent<GridTile>();
            var go = crop.GetComponent<GameObjectComponent>().GameObject;
            var currentStage = self.GetCurrentStage();
            if (crop.LastStage != currentStage || forceDisplay)
            {
                crop.LastStage = currentStage;
                
                if (go != null)
                {
                    UnityEngine.Object.Destroy(go);
                }

                var prefab = await AssetComponent.LoadAsync<GameObject>(crop.Config.GrowthPrefabs.Length == 1? crop.Config.GrowthPrefabs[0].StringToAB()
                        : crop.Config.GrowthPrefabs[currentStage].StringToAB());

                crop.GetComponent<GameObjectComponent>().GameObject = go = UnityEngine.Object.Instantiate(prefab,
                    new Vector3(tile.GridX + 0.5f, tile.GridY + 0.5f, 0), Quaternion.identity,
                    GlobalComponent.Instance.CropRoot);

                if (crop.Config.CanFade == 1)
                {
                    if (crop.Config.FadeParent == 1)
                    {
                        go.tag = TagManager.FadeObject;
                    }
                    else
                    {
                        var childCount = go.transform.childCount;
                        for (int i = 0; i < childCount; i++)
                        {
                            go.transform.GetChild(i).tag = TagManager.FadeObject;
                        }
                    }
                }
                else
                {
                    go.tag = TagManager.Crop;
                }
                
                
                go.AddComponent<MonoBridge>().BelongToEntityId = crop.InstanceId;

                if (go.GetComponent<ReferenceCollector>() != null)
                {
                    self.SpriteRenderer = go.GetComponentFormRC<SpriteRenderer>("Sprite");
                    self.BoxCollider2D = go.GetComponent<BoxCollider2D>();

                    Sprite sprite = await IconHelper.LoadIconSpriteAsync(crop.Config.GrowthSprites[currentStage]);
                    self.SpriteRenderer.sprite = sprite;

                    //修改boxcollider尺寸
                    if (self.BoxCollider2D.isTrigger) //不是Trigger说明提前设置好了对应的大小
                    {
                        Vector2 newSize = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y);
                        self.BoxCollider2D.size = newSize;
                        self.BoxCollider2D.offset = new Vector2(0, sprite.bounds.center.y);
                    }
                }
            }
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
                    return currentStage;
                }

                dayCount -= crop.Config.GrowthDays[i];
            }

            return currentStage;
        }
    }
}