using System.Collections;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// 将ETTask 转为 Coroutine
    /// </summary>
    public static class ETTaskToCoroutine
    {
        public static Coroutine AsCoroutine(this ETTask task)
        {
            return CoroutineHelper.Instance.StartCoroutine(AsEnumerator(task));
        }

        private static IEnumerator AsEnumerator(ETTask task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
        }
    }
}