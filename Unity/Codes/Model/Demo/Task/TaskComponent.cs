using System.Collections.Generic;
#if SERVER
using MongoDB.Bson.Serialization.Attributes;
#endif

namespace ET
{
    [ComponentOf()]
    [ChildType(typeof(TaskInfo))]
#if SERVER
    public class TaskComponent : Entity,IAwake,IDestroy,IDeserialize,ITransfer,IUnitCache
#else
    public class TaskComponent: Entity, IAwake, IDestroy
#endif
    {
#if SERVER
        [BsonIgnore]
#endif
        public SortedDictionary<int, TaskInfo> TaskInfoDict = new SortedDictionary<int, TaskInfo>();

#if !SERVER
        public List<TaskInfo> TaskInfoList = new List<TaskInfo>();
#endif
        
#if SERVER
        [BsonIgnore]
        public HashSet<int> CurrentTaskSet = new HashSet<int>();
#endif
        
#if SERVER
        [BsonIgnore]
        public M2C_UpdateTaskInfo Message = new M2C_UpdateTaskInfo();
#endif
    }
}