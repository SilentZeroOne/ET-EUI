namespace ET
{
    public class TaskInfoAwakeSystem: AwakeSystem<TaskInfo>
    {
        public override void Awake(TaskInfo self)
        {

        }
    }

    public class TaskInfoDestroySystem: DestroySystem<TaskInfo>
    {
        public override void Destroy(TaskInfo self)
        {

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

        
    }
}