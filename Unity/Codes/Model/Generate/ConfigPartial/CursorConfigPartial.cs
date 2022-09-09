namespace ET
{
    public partial class CursorConfigCategory
    {
        public CursorConfig GetDefaultCursor()
        {
            return Get(1);
        }

        public CursorConfig GetCursorConfigByItemType(int itemType)
        {
            foreach (var config in this.list)
            {
                foreach (var correspondItemType in config.CorrespondItemType)
                {
                    if (correspondItemType == itemType)
                    {
                        return config;
                    }
                }
            }

            return null;
        }
    }
}