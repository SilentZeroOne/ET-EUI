using System.Collections;
using UnityEngine;

namespace ET
{
    public static class YieldInstructionToETTask
    {
        // public static ETTask<T> GetAwaiter<T>(this T enumerator) where T : YieldInstruction
        // {
        //     return ToAwaiter(enumerator);
        // }
        //
        // private static ETTask<T> ToAwaiter<T>(T enumerator) where T : YieldInstruction
        // {
        //     var task = ETTask<T>.Create();
        //     CoroutineHelper.Instance.StartCoroutine(ToAwaiter(enumerator, task));
        //     return task;
        // }
        //
        // private static IEnumerator ToAwaiter<T>(T enumerator, ETTask<T> etTask) where T : YieldInstruction
        // {
        //     yield return enumerator;
        //     etTask.SetResult(enumerator);
        // }
    }
}