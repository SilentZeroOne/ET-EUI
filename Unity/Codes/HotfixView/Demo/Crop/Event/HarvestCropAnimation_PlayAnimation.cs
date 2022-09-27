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

            CreateParticle(a, go).Coroutine();
        }

        private async ETTask CreateParticle(HarvestCropAnimation a , GameObject go)
        {
            GameObject particleObj = null;
            if (a.Crop.Config.HasParticleEffect == 1 && !string.IsNullOrEmpty(a.Crop.Config.ParticlePrefab))
            {
                var pos = a.Crop.Config.ParticlePos;
                var particlePos = new Vector3(pos[0], pos[1], pos[2]);

                particleObj = MyPoolObjHelper.GetObjectFromPool(a.Crop.Config.ParticlePrefab, true, 5);
                particleObj.transform.position = go.transform.position + particlePos;
            }

            await TimerComponent.Instance.WaitAsync(1500);
            
            //回收粒子特效
            if (particleObj != null)
                MyPoolObjHelper.ReturnObjectToPool(particleObj);
        }
    }
}