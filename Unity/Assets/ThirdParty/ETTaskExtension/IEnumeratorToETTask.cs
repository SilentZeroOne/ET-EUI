using System.Collections;

namespace ET
{
    /// <summary>
    /// 迭代器转ETTask
    /// </summary>
    public static class IEnumeratorToETTask
    {
        public static ETTask<T> GetAwaiter<T>(this T enumerator) where T : IEnumerator
        {
            return ToAwaiter(enumerator);
        }

        private static ETTask<T> ToAwaiter<T>(T enumerator) where T : IEnumerator
        {
            var task = ETTask<T>.Create();
            CoroutineHelper.Instance.StartCoroutine(ToAwaiter(enumerator, task));
            return task;
        }

        private static IEnumerator ToAwaiter<T>(T enumerator, ETTask<T> etTask) where T : IEnumerator
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
            etTask.SetResult(enumerator);
        }
    }
}