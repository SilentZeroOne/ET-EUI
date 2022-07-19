namespace ET
{
    public static class TaskHelper
    {
        public static async ETTask<int> GetTaskReward(Scene zoneScene, int taskConfigId)
        {
            await ETTask.CompletedTask;
            return 1;
        }
    }
}