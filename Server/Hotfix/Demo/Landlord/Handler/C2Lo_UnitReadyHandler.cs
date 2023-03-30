using System;

namespace ET
{
    public class C2Lo_UnitReadyHandler: AMActorRpcHandler<Scene, C2Lo_UnitReady, Lo2C_UnitReady>
    {
        protected override async ETTask Run(Scene scene, C2Lo_UnitReady request, Lo2C_UnitReady response, Action reply)
        {
            LandMatchComponent landMatchComponent = scene.GetComponent<LandMatchComponent>();
            if (landMatchComponent == null)
            {
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                return;
            }

            Room room = landMatchComponent.GetWaitingRoom(request.UnitId);
            if (room != null)
            {
                if (room.SetReady(request.UnitId, request.Ready == 1))
                {
                    reply();
                    return;
                }
            }

            response.Error = ErrorCode.ERR_CantReadyError;
            reply();
            await ETTask.CompletedTask;
        }
    }
}