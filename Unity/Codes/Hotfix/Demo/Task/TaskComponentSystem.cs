using System.Linq;

namespace ET
{
    public class TaskComponentAwakeSystem: AwakeSystem<TaskComponent>
    {
        public override void Awake(TaskComponent self)
        {

        }
    }

    public class TaskComponentDestroySystem: DestroySystem<TaskComponent>
    {
        public override void Destroy(TaskComponent self)
        {

        }
    }

    [FriendClass(typeof (TaskComponent))]
    [FriendClass(typeof(TaskInfo))]
    public static class TaskComponentSystem
    {
        public static int GetTaskInfoCount(this TaskComponent self)
        {
            self.TaskInfoList.Clear();
            self.TaskInfoList = self.TaskInfoDict.Values.Where((a) => !a.IsTaskState(TaskState.Received)).ToList();
            self.TaskInfoList.Sort((a, b) => b.TaskState - a.TaskState);
            return self.TaskInfoList.Count;
        }

        public static TaskInfo GetTaskInfoByIndex(this TaskComponent self, int index)
        {
            if (index < 0 || index > self.TaskInfoList.Count)
            {
                return null;
            }

            return self.TaskInfoList[index];
        }

        public static TaskInfo GetTaskInfoByConfigId(this TaskComponent self, int configId)
        {
            self.TaskInfoDict.TryGetValue(configId, out var taskInfo);
            return taskInfo;
        }

        public static void AddOrUpdateTaskInfo(this TaskComponent self, TaskInfoProto proto)
        {
            TaskInfo taskInfo = self.GetTaskInfoByConfigId(proto.ConfigId);
            if (taskInfo == null)
            {
                taskInfo = self.AddChild<TaskInfo>();
                self.TaskInfoDict.Add(proto.ConfigId, taskInfo);
            }

            taskInfo.FromMessage(proto);
            Game.EventSystem.Publish(new EventType.UpdateTaskInfo() { ZoneScene = self.ZoneScene() });
        }

        public static bool IsExistTaskComplete(this TaskComponent self)
        {
            foreach (var taskInfo in self.TaskInfoDict.Values)
            {
                if (taskInfo.IsTaskState(TaskState.Complete))
                {
                    return true;
                }
            }

            return false;
        }


    }
}