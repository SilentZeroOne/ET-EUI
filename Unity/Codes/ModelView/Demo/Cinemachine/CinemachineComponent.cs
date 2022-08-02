using Cinemachine;
using UnityEngine;

namespace ET
{
    [ComponentOf()]
    public class CinemachineComponent: Entity, IAwake<GameObject>, IDestroy
    {
        public CinemachineConfiner Confiner;
    }
}