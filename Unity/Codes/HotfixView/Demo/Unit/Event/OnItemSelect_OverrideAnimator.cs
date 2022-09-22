using ET.EventType;
using UnityEngine;

namespace ET
{
    public class OnItemSelect_OverrideAnimator : AEvent<OnItemSelected>
    {
        protected override void Run(OnItemSelected a)
        {
            if (!((ItemType)a.Item.Config.ItemType == ItemType.Commodity || (ItemType)a.Item.Config.ItemType == ItemType.Seed))
            {
                a.Carried = false;
            }
            
            Unit unit = UnitHelper.GetMyUnitFromZoneScene(a.ZoneScene);
            var config = AnimatorControllerConfigCategory.Instance.GetConfigByNameAndStatus(AnimatorType.Arm.ToString(),
                a.Carried? (int)AnimatorStatus.Carried : (int)AnimatorStatus.None);
            unit.GetComponent<AnimatorComponent>().OverrideAnimator(AnimatorType.Arm, config.OverrideControllerName);

            var holdItemSprite = unit.GetComponent<GameObjectComponent>().GameObject.GetComponentFormRC<SpriteRenderer>("HoldItem");
            holdItemSprite.color = new Color(1, 1, 1, a.Carried? 1 : 0);
            holdItemSprite.sprite = IconHelper.LoadIconSprite(a.Item.Config.ItemOnWorldSprite);
        }
    }
}