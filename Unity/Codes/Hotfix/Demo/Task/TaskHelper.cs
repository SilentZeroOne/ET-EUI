using System;

namespace ET
{
    public static class TaskHelper
    {
        public static async ETTask<int> GetTaskReward(Scene zoneScene, int taskConfigId)
        {
            if (!TaskConfigCategory.Instance.Contain(taskConfigId))
            {
                return ErrorCode.ERR_TaskConfigNotExist;
            }

            M2C_ReceiveTaskReward m2CReceiveTaskReward = null;
            try
            {
                m2CReceiveTaskReward = (M2C_ReceiveTaskReward)await zoneScene.GetComponent<SessionComponent>().Session
                        .Call(new C2M_ReceiveTaskReward() { TaskConfigId = taskConfigId });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }

            return m2CReceiveTaskReward.Error;
        }
    }
}