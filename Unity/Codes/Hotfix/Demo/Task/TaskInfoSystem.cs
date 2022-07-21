namespace ET
{
    public class TaskInfoAwakeSystem: AwakeSystem<TaskInfo,int>
    {
        public override void Awake(TaskInfo self,int configId)
        {
            self.ConfigId = configId;
            self.TaskProgress = 0;
            self.TaskState = (int)TaskState.Doing;
        }
    }

    public class TaskInfoDestroySystem: DestroySystem<TaskInfo>
    {
        public override void Destroy(TaskInfo self)
        {
            self.ConfigId = 0;
            self.TaskProgress = 0;
            self.TaskState = (int)TaskState.None;
        }
    }

    [FriendClass(typeof (TaskInfo))]
    public static class TaskInfoSystem
    {
        public static bool IsTaskState(this TaskInfo self,TaskState state)
        {
            return self.TaskState == (int)state;
        }

        public static void SetTaskState(this TaskInfo self, TaskState state)
        {
            self.TaskState = (int)state;
        }

        public static void UpdateProgress(this TaskInfo self, int count)
        {
            var taskType = TaskConfigCategory.Instance.Get(self.ConfigId).TaskActionType;
            var config = TaskActionConfigCategory.Instance.Get(taskType);
            if (config.TaskProgressType == (int)TaskProgressType.Add)
            {
                self.TaskProgress += count;
            }
            else if (config.TaskProgressType == (int)TaskProgressType.Sub)
            {
                self.TaskProgress -= count;
            }
            else if (config.TaskProgressType == (int)TaskProgressType.Update)
            {
                self.TaskProgress = count;
            }
        }

        public static void TryCompleteTask(this TaskInfo self)
        {
            if (!self.IsCompleteProgress() || !self.IsTaskState(TaskState.Doing))
            {
                return;
            }

            self.TaskState = (int)TaskState.Complete;
        }

        public static bool IsCompleteProgress(this TaskInfo self)
        {
            return self.TaskProgress >= TaskConfigCategory.Instance.Get(self.ConfigId).TaskTargetCount;
        }

        public static void FromMessage(this TaskInfo self, TaskInfoProto proto)
        {
            self.ConfigId = proto.ConfigId;
            self.TaskProgress = proto.TaskProgress;
            self.TaskState = proto.TaskState;
        }

        public static TaskInfoProto ToMessage(this TaskInfo self)
        {
            return new TaskInfoProto()
            {
                ConfigId = self.ConfigId,
                TaskProgress = self.TaskProgress,
                TaskState = self.TaskState
            };
        }
        
    }
}