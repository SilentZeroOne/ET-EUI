using ET.EventType;
using UnityEngine;

namespace ET
{
    public class HarvestCropParticle_PlayParticle: AEvent<HarvestCropParticle>
    {
        protected override void Run(HarvestCropParticle a)
        {
            GameObject go = a.Crop.GetComponent<GameObjectComponent>().GameObject;
            CreateParticle(a, go).Coroutine();
        }

        private async ETTask CreateParticle(HarvestCropParticle a , GameObject go)
        {
            GameObject particleObj = null;
            if (!string.IsNullOrEmpty(a.Crop.Config.ParticlePrefab))
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