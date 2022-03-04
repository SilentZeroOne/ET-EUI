using System;

namespace ET
{
    public class G2L_DisconnectGateUnitHandler : AMActorRpcHandler<Scene,L2G_DisconnectGateUnit,G2L_DisconnectGateUnit>
    {
        protected override async ETTask Run(Scene scene, L2G_DisconnectGateUnit request, G2L_DisconnectGateUnit response, Action reply)
        {
            var accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateLoginLock,accountId.GetHashCode()))
            {
                var playerComponent = scene.GetComponent<PlayerComponent>();
                var gateUnit = playerComponent.Get(accountId);
                
                if (gateUnit == null)
                {
                    reply();
                    return;
                }
                
                playerComponent.Remove(accountId);
                gateUnit.Dispose(); //临时处理
            }
            
            reply();
        }
    }
}