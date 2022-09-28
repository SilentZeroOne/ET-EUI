using ET.EventType;
using UnityEngine;

namespace ET
{
    public class HarvestCropAnimation_PlayAnimation: AEvent<HarvestCropAnimation>
    {
        protected override void Run(HarvestCropAnimation a)
        {
            var animator = a.Crop.GetComponent<AnimatorComponent>() ?? a.Crop.AddComponent<AnimatorComponent>();

            Unit player = UnitHelper.GetMyUnitFromZoneScene(a.ZoneScene);
            GameObject go = a.Crop.GetComponent<GameObjectComponent>().GameObject;

            if (player.GetComponent<GameObjectComponent>().GameObject.transform.position.x < go.transform.position.x)
            {
                animator.Play(MotionType.RotateRight);
            }
            else
            {
                animator.Play(MotionType.RotateLeft);
            }
        }
    }
}