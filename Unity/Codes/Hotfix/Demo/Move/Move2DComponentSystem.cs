using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [Timer(TimerType.Move2DTimer)]
    public class Move2DTimer: ATimer<Move2DComponent>
    {
        public override void Run(Move2DComponent self)
        {
            try
            {
                self.MoveForward(false);
            }
            catch (Exception e)
            {
                Log.Error($"2d move timer error: {self.Id}\n{e}");
            }
        }
    }
    
    public class Move2DComponentAwakeSystem: AwakeSystem<Move2DComponent>
    {
        public override void Awake(Move2DComponent self)
        {
            self.StartPos = Vector3.zero;
            self.StartTime = 0;
            self.BeginTime = 0;
            self.NeedTime = 0;

            self.PreStep = null;
            self.CurrentStep = null;

            self.MoveTimer = 0;
            self.Steps = null;
            self.Speed = 0;
        }
    }

    public class Move2DComponentDestroySystem: DestroySystem<Move2DComponent>
    {
        public override void Destroy(Move2DComponent self)
        {
            self.Clear();
        }
    }

    public class Move2DComponentFixedUpdateSystem: FixedUpdateSystem<Move2DComponent>
    {
        public override void FixedUpdate(Move2DComponent self)
        {
            if (self.Steps != null)
            {
                self.MoveForward(false);
            }
        }
    }
    
    [FriendClassAttribute(typeof(ET.Move2DComponent))]
    public static class Move2DComponentSystem
    {
        public static async ETTask<bool> MoveToAsync(this Move2DComponent self, Stack<MovementStep> steps, float speed, ETCancellationToken cancellationToken = null)
        {
            self.Stop();
            
            self.Steps = steps;
            self.Speed = speed;
            
            ETTask<bool> tcs = ETTask<bool>.Create(true);
            self.Callback = (ret) => { tcs.SetResult(ret); };
            
            self.StartMove();

            void CancelAction()
            {
                self.Stop();
            }
            
            bool moveRet;
            try
            {
                cancellationToken?.Add(CancelAction);
                moveRet = await tcs;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction);
            }

            return moveRet;
        }

        public static void MoveForward(this Move2DComponent self, bool needCancel)
        {
            var gameTimeComponent = self.ZoneScene().GetComponent<GameTimeComponent>();
            Unit unit = self.GetParent<Unit>();
            
            //var timeNow = gameTimeComponent.GameTime;
            var moveTime = TimeHelper.ClientNow() - self.StartTime;

            while (true)
            {
                if (moveTime <= 0)
                {
                    return;
                }

                //超过应该移动的时间  直接瞬移到该点
                if (moveTime > self.NeedTime)
                {
                    unit.Position = self.CurrentStep.Position + Vector2.one / 2;
                    //TODO: 调整面向
                }
                else
                {
                    //计算移动插值
                    float amount = moveTime * 1f / self.NeedTime;
                    if (amount > 0)
                    {
                        Vector2 newPos = Vector2.Lerp(self.StartPos, self.CurrentStep.Position + Vector2.one / 2, amount);
                        unit.Position = newPos;
                    }
                    //TODO: 调整面向
                }

                moveTime -= self.NeedTime;

                if (moveTime < 0)//这个点还没走完 等下一帧
                {
                    return;
                }
                
                //到这 这个点就走完了

                if (self.Steps.Count <= 0)//最后一个点了
                {
                    unit.Position = self.CurrentStep.Position + Vector2.one / 2;
                    Action<bool> callback = self.Callback;
                    self.Callback = null;

                    self.Clear();
                    callback?.Invoke(!needCancel);
                    return;
                }
                
                self.SetNextStep();
            }
        }

        public static void StartMove(this Move2DComponent self)
        {
            var gameTimeComponent = self.ZoneScene().GetComponent<GameTimeComponent>();
            self.BeginTime = TimeHelper.ClientNow();
            self.StartTime = self.BeginTime;
            
            self.SetNextStep();
            //self.MoveTimer = TimerComponent.Instance.NewFrameTimer(TimerType.Move2DTimer, self);
        }

        public static void SetNextStep(this Move2DComponent self)
        {
            Unit unit = self.GetParent<Unit>();
            self.StartPos = unit.Position;
            self.PreStep = self.CurrentStep ?? new MovementStep() { GridX = (int)self.StartPos.x, GridY = (int)self.StartPos.y };
            
            if (self.Steps.Count > 0)
            {
                self.CurrentStep = self.Steps.Pop();
                Vector2 faceV = self.GetFaceV();
                var distance = faceV.magnitude;

                self.StartTime += self.NeedTime;

                self.NeedTime = (long)(distance / self.Speed * 1000);
            }
        }
        
        private static Vector2 GetFaceV(this Move2DComponent self)
        {
            return self.CurrentStep.Position - self.PreStep.Position;
        }

        public static void Stop(this Move2DComponent self)
        {
            if (self.Steps != null && self.Steps.Count > 0)
            {
                self.MoveForward(true);
            }
            
            self.Clear();
        }
        
        public static void Clear(this Move2DComponent self)
        {
            self.StartPos = Vector3.zero;
            self.StartTime = 0;
            self.BeginTime = 0;
            self.NeedTime = 0;

            self.PreStep = null;
            self.CurrentStep = null;
            
            TimerComponent.Instance?.Remove(ref self.MoveTimer);
            self.Steps = null;
            self.Speed = 0;
            
            if (self.Callback != null)
            {
                Action<bool> callback = self.Callback;
                self.Callback = null;
                callback.Invoke(false);
            }
        }
    }
}