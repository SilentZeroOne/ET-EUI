using System.Collections.Generic;

namespace ET
{
    public partial class TaskConfigCategory
    {
        public Dictionary<int, List<int>> BeforeTaskConfigDictionary = new Dictionary<int, List<int>>();

        public override void AfterEndInit()
        {
            base.AfterEndInit();

            foreach (var config in this.list)
            {
                if (!this.BeforeTaskConfigDictionary.ContainsKey(config.TaskBeforeId))
                {
                    this.BeforeTaskConfigDictionary.Add(config.TaskBeforeId,new List<int>());
                }
                this.BeforeTaskConfigDictionary[config.TaskBeforeId].Add(config.Id);
            }
        }

        public List<int> GetAfterTaskIdListByBeforeId(int beforeConfigId)
        {
            if (this.BeforeTaskConfigDictionary.TryGetValue(beforeConfigId,out var taskList))
            {
                return taskList;
            }

            return null;
        }
    }
}