namespace ET
{
    public enum EntryType
    {
        Common = 1,
        Special = 2
    }
#if SERVER
    public class AttributeEntry: Entity,IAwake,IDestroy,ISerializeToEntity
#else
    public class AttributeEntry: Entity,IAwake,IDestroy
#endif
    {
        /// <summary>
        /// 词条属性类型
        /// </summary>
        public int Key;

        /// <summary>
        /// 词条属性值
        /// </summary>
        public long Value;

        /// <summary>
        /// 词条类型
        /// </summary>
        public EntryType Type;
    }
}