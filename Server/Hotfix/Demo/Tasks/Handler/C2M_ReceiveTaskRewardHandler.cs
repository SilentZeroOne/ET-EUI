using System;

namespace ET
{
    public class C2M_ReceiveTaskRewardHandler: AMActorLocationRpcHandler<Unit, C2M_ReceiveTaskReward, M2C_ReceiveTaskReward>
    {
        protected override async ETTask Run(Unit unit, C2M_ReceiveTaskReward request, M2C_ReceiveTaskReward response, Action reply)
        {
            TaskComponent taskComponent = unit.GetComponent<TaskComponent>();

            int errCode = taskComponent.TryReceiveTaskReward(request.TaskConfigId);
            if (errCode != ErrorCode.ERR_Success)
            {
                response.Error = errCode;
                reply();
                return;
            }
            
            taskComponent.ReceiveTaskRewardState(unit,request.TaskConfigId);
            
            unit.GetComponent<NumericComponent>()[NumericType.Gold] += TaskConfigCategory.Instance.Get(request.TaskConfigId).TaskRewardCount;
            
            reply();
            await ETTask.CompletedTask;
        }
    }
}