using System;
using UnityEngine;

namespace ET
{
    public class HotfixStart: MonoBehaviour
    {
        private void Start()
        {
            // try
            // {
            //     Log.Error("Mono Start");
            //     IStaticMethod start = new MonoStaticMethod(CodeLoader.Instance.assembly, "ET.Entry", "Start");
            //     start.Run();
            //     // CodeLoader.Instance.Update += Game.Update;
            //     // CodeLoader.Instance.LateUpdate += Game.LateUpdate;
            //     // CodeLoader.Instance.OnApplicationQuit += Game.Close;
				        //     //
				        //     //
            //     // Game.EventSystem.Add(CodeLoader.Instance.GetHotfixTypes());
            //     //
				        //     //
            //     // Game.EventSystem.Publish(new EventType.AppStart());
            // }
            // catch (Exception e)
            // {
            //     Log.Error(e);
            // }
        }
    }
}