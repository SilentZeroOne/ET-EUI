﻿namespace ET
{
    public class TaskComponentAwakeSystem: AwakeSystem<TaskComponent>
    {
        public override void Awake(TaskComponent self)
        {
            self.Awake();
        }
    }

    public class TaskComponentDestroySystem: DestroySystem<TaskComponent>
    {
        public override void Destroy(TaskComponent self)
        {
            foreach (var taskInfo in self.TaskInfoDict.Values)
            {
                taskInfo?.Dispose();
            }
            self.TaskInfoDict.Clear();
            self.CurrentTaskSet.Clear();
        }
    }

    [FriendClass(typeof(TaskInfo))]
    public class TaskComponentDeserializeSystem: DeserializeSystem<TaskComponent>
    {
        public override void Deserialize(TaskComponent self)
        {
            foreach (Entity entity in self.Children.Values)
            {
                TaskInfo taskInfo = entity as TaskInfo;
                self.TaskInfoDict.Add(taskInfo.ConfigId, taskInfo);
                if (!taskInfo.IsTaskState(TaskState.Received))
                {
                    self.CurrentTaskSet.Add(taskInfo.ConfigId);
                }
            }
        }
    }

    [FriendClass(typeof (TaskComponent))]
    [FriendClass(typeof(TaskInfo))]
    public static class TaskComponentSystem
    {
        public static void Awake(this TaskComponent self)
        {
            if (self.TaskInfoDict.Count == 0)
            {
                self.UpdateAfterTaskInfo(0, false);
            }
        }

        public static void UpdateAfterTaskInfo(this TaskComponent self, int beforeTaskConfigId, bool isNoticeClient = true)
        {
            self.CurrentTaskSet.Remove(beforeTaskConfigId);
            var configList = TaskConfigCategory.Instance.GetAfterTaskIdListByBeforeId(beforeTaskConfigId);
            if (configList == null) return;

            foreach (var configId in configList)
            {
                self.CurrentTaskSet.Add(configId);
                int count = self.GetTaskInitProgressCount(configId);
                self.AddOrUpdateTaskInfo(configId, count, isNoticeClient);
            }
        }

        public static int GetTaskInitProgressCount(this TaskComponent self, int taskConfigId)
        {
            var config = TaskConfigCategory.Instance.Get(taskConfigId);

            if (config.TaskActionType == (int)TaskActionType.UpLevel)
            {
                return self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt(NumericType.Level);
            }

            return 0;
        }

        public static void AddOrUpdateTaskInfo(this TaskComponent self, int taskConfigId, int count, bool isNoticeClient = true)
        {
            if (!self.TaskInfoDict.TryGetValue(taskConfigId, out var taskInfo))
            {
                taskInfo = self.AddChild<TaskInfo, int>(taskConfigId);
                self.TaskInfoDict.Add(taskConfigId, taskInfo);
            }

            taskInfo.UpdateProgress(count);
            taskInfo.TryCompleteTask();
            if (isNoticeClient)
            {
                TaskNoticeHelper.SyncTaskInfo(self.GetParent<Unit>(), taskInfo, self.Message);
            }
        }

        public static void TriggerTaskAction(this TaskComponent self,TaskActionType taskActionType, int count, int targetId = 0)
        {
            foreach (var taskConfigId in self.CurrentTaskSet)
            {
                TaskConfig config = TaskConfigCategory.Instance.Get(taskConfigId);
                if (config.TaskActionType == (int)taskActionType && config.TaskTargetId == targetId)
                {
                    self.AddOrUpdateTaskInfo(taskConfigId, count);
                }
            }
        }
    }
}