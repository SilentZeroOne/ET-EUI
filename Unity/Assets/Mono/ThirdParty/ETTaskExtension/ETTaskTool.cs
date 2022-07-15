using UnityEngine;

namespace ET
{
    //不推荐直接使用  除非使用条件很宽松 
    public class ETTaskTool
    {
        /// <summary>
        /// 等待指定毫秒
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static YieldInstruction Delay(int time)
        {
            return new WaitForSeconds(time / 1000f);
        }

        /// <summary>
        /// 等待一帧
        /// </summary>
        /// <returns></returns>
        public static DelayFrame DelayFrame()
        {
            return new DelayFrame();
        }

        /// <summary>
        /// 等待指定帧数
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static DelayFrame DelayFrame(int frame)
        {
            return new DelayFrame(frame);
        }
    }
}