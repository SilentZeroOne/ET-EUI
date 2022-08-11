﻿using System;
using System.Collections;
using UnityEngine;

namespace ET
{
    public static class AsyncOperationToETTask
    {
        //有了这个方法就可以直接Await Unity的AsyncOperation
        public static async ETTask GetAwaiter(this AsyncOperation asyncOperation)
        {
            ETTask task = ETTask.Create(true);
            asyncOperation.completed += _ => { task.SetResult(); };
            await task;
        }

        // public static ETTask<T> GetAwaiter<T>(this T enumerator) where T : AsyncOperation
        // {
        //     return ToAwaiter(enumerator, null);
        // }
        //
        // public static ETTask<T> GetAwaiter<T>(this T enumerator, Action<T> update) where T : AsyncOperation
        // {
        //     return ToAwaiter(enumerator, update);
        // }
        //
        // private static ETTask<T> ToAwaiter<T>(T enumerator, Action<T> updata) where T : AsyncOperation
        // {
        //     var task = ETTask<T>.Create();
        //     CoroutineHelper.Instance.StartCoroutine(ToAwaiter(enumerator, task, updata));
        //     return task;
        // }
        //
        // private static IEnumerator ToAwaiter<T>(T enumerator, ETTask<T> etTask, Action<T> update) where T : AsyncOperation
        // {
        //     while (enumerator.isDone)
        //     {
        //         update?.Invoke(enumerator);
        //         yield return null;
        //     }
        //     etTask.SetResult(enumerator);
        // }
    }
}