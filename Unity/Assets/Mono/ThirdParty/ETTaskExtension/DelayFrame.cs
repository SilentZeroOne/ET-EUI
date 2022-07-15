using System.Collections;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// 等待一定的帧率
    /// </summary>
    public sealed class DelayFrame
    {
        private int Frame { get; }

        public DelayFrame(int frame)
        {
            this.Frame = frame;
        }

        public DelayFrame()
        {
            this.Frame = 1;
        }

        public ETTask GetAwaiter()
        {
            ETTask task = ETTask.Create();
            CoroutineHelper.Instance.StartCoroutine(ToAwaiter(task));
            return task;
        }

        private IEnumerator ToAwaiter(ETTask etTask)
        {
            if(this.Frame == 1)
            {
                yield return null;
            }
            else
            {
                for (int i = 0; i < this.Frame; i++)
                {
                    yield return null;
                }
            }

            etTask.SetResult();
        }
    }
}