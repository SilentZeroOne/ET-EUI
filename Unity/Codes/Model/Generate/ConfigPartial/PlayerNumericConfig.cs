namespace ET
{
    public partial class PlayerNumericConfigCategory: ProtoObject, IMerge
    {
        public PlayerNumericConfig GetConfigByIndex(int index)
        {
            return this.showList[index];
        }

        public int GetShowConfigCount()
        {
            return this.showList.Count;
        }
    }
}