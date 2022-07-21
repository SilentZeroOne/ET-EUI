namespace ET
{
    [FriendClass(typeof(TaskComponent))]
    public static class TaskNoticeHelper
    {
        public static void SyncTaskInfo(Unit unit,TaskInfo taskInfo, M2C_UpdateTaskInfo message)
        {
            message.TaskInfoProto = taskInfo.ToMessage();
            MessageHelper.SendToClient(unit, message);
        }

        public static void SyncAllTaskInfo(Unit unit)
        {
            M2C_AllTaskInfoList m2CAllTaskInfoList = new M2C_AllTaskInfoList();
            TaskComponent taskComponent = unit.GetComponent<TaskComponent>();
            foreach (var taskInfo in taskComponent.TaskInfoDict.Values)
            {
                m2CAllTaskInfoList.TaskInfoProtoList.Add(taskInfo.ToMessage());
            }

            MessageHelper.SendToClient(unit, m2CAllTaskInfoList);
        }
    }
}