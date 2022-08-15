using UnityEngine;

namespace ET
{
    /// <summary>
    /// 链接ET和Unity场景内物品的桥梁
    /// </summary>
    public class MonoBridge: MonoBehaviour
    {
        /// <summary>
        /// 属于哪个Entity,InstanceId
        /// </summary>
        public long BelongToEntityId;
    }
}