#if !SERVER
using UnityEngine;
#endif

namespace ET
{
    public class ProductionAwakeSystem: AwakeSystem<Production>
    {
        public override void Awake(Production self)
        {

        }
    }

    public class ProductionDestroySystem: DestroySystem<Production>
    {
        public override void Destroy(Production self)
        {

        }
    }

    [FriendClass(typeof (Production))]
    public static class ProductionSystem
    {
        public static void FromMessage(this Production self, ProductionProto proto)
        {
            self.Id = proto.Id;
            self.ConfigId = proto.ConfigId;
            self.ProductionState = proto.ProductionState;
            self.StartTime = proto.StartTime;
            self.TargetTime = proto.TargetTime;
        }

        public static ProductionProto ToMessage(this Production self)
        {
            return new ProductionProto()
            {
                Id = self.Id,
                ConfigId = self.ConfigId,
                StartTime = self.StartTime,
                ProductionState = self.ProductionState,
                TargetTime = self.TargetTime
            };
        }

        public static bool IsMakingState(this Production self)
        {
            return self.ProductionState == (int)ProductionState.Making;
        }

        public static bool IsMakeTimeOver(this Production self)
        {
            return TimeHelper.ServerNow() >= self.TargetTime;
        }

        public static void Reset(this Production self)
        {
            self.ConfigId = 0;
            self.TargetTime = 0;
            self.ProductionState = (int)ProductionState.Received;
        }

        public static float GetRemainingValue(this Production self)
        {
            long remainTime = self.TargetTime - TimeHelper.ServerNow();
            if (remainTime <= 0)
            {
                return 0.0f;
            }

            long totalTime = self.TargetTime - self.StartTime;
            return remainTime / (float)totalTime;
        }
        
#if !SERVER
        public static string GetRemainingTimeStr(this Production self)
        {
            long remainTime = self.TargetTime - TimeHelper.ServerNow();
            if (remainTime <= 0)
            {
                return "0时0分0秒";
            }

            remainTime /= 1000;
            float h = Mathf.FloorToInt(remainTime / 3600.0f);
            float m = Mathf.FloorToInt(remainTime / 60.0f - h * 60f);
            float s = Mathf.FloorToInt(remainTime - m * 60 - h * 3600f);
            return h.ToString("00") + "小时" + m.ToString("00") + "分" + s.ToString("00") + "秒";
        }
#endif
    }
}