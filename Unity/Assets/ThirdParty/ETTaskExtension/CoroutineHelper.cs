using UnityEngine;

namespace ET
{
    /// <summary>
    /// 携程帮助类。自动创建，不可销毁，在层次结构中不可见。
    /// </summary>
    public class CoroutineHelper: MonoBehaviour
    {
        public static CoroutineHelper Instance { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateInstance()
        {
            Instance = new GameObject("CoroutineHelper (Awaiters)").AddComponent<CoroutineHelper>();
            GameObject o = Instance.gameObject;
            o.hideFlags = HideFlags.HideInHierarchy;
            DontDestroyOnLoad(o);
        }
    }
}