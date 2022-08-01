using Cinemachine;

namespace ET
{
    [ComponentOf()]
    public class CinemachineComponent: Entity, IAwake, IDestroy
    {
        public CinemachineConfiner Confiner;
    }
}