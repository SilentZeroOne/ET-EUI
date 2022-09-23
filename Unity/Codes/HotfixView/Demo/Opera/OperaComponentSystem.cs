using System;
using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class OperaComponentAwakeSystem : AwakeSystem<OperaComponent>
    {
        public override void Awake(OperaComponent self)
        {
            self.mapMask = LayerMask.GetMask("Map");
        }
    }

    [ObjectSystem]
    public class OperaComponentUpdateSystem : UpdateSystem<OperaComponent>
    {
        public override void Update(OperaComponent self)
        {
            self.Update();
        }
    }
    
    public class OperaComponentFixedUpdateSystem : FixedUpdateSystem<OperaComponent>
    {
        public override void FixedUpdate(OperaComponent self)
        {
            self.FixedUpdate();
        }
    }

    [FriendClass(typeof(OperaComponent))]
    [FriendClassAttribute(typeof(ET.Unit))]
    public static class OperaComponentSystem
    {
        public static void Update(this OperaComponent self)
        {
            

            // if (InputHelper.GetMouseButtonDown(1))
            // {
            //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //     RaycastHit hit;
            //     if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
            //     {
            //         self.ClickPoint = hit.point;
            //         self.frameClickMap.X = self.ClickPoint.x;
            //         self.frameClickMap.Y = self.ClickPoint.y;
            //         self.frameClickMap.Z = self.ClickPoint.z;
            //         self.ZoneScene().GetComponent<SessionComponent>().Session.Send(self.frameClickMap);
            //     }
            // }
            //
            // // KeyCode.R
            // if (InputHelper.GetKeyDown(114))
            // {
            //     CodeLoader.Instance.LoadLogic();
            //     Game.EventSystem.Add(CodeLoader.Instance.GetHotfixTypes());
            //     Game.EventSystem.Load();
            //     Log.Debug("hot reload success!");
            // }
            //
            // // KeyCode.T
            // if (InputHelper.GetKeyDown(116))
            // {
            //     C2M_TransferMap c2MTransferMap = new C2M_TransferMap();
            //     self.ZoneScene().GetComponent<SessionComponent>().Session.Call(c2MTransferMap).Coroutine();
            // }

            //TODO:正式发布时删除
            //游戏天数加一
            if (InputHelper.GetKeyDown(KeyCode.T))
            {
                self.ZoneScene().GetComponent<GameTimeComponent>().FlashDay();
            }
        }

        public static void FixedUpdate(this OperaComponent self)
        {
            Unit player = self.GetParent<Unit>();
            if (player.InputDisabled) return;
            
            var inputX = InputHelper.GetXAxisRaw();
            var inputY = InputHelper.GetYAxisRaw();

            if (inputX != 0 && inputY != 0)
            {
                inputX *= 0.6f;
                inputY *= 0.6f;
            }

            player.GetComponent<RigidBody2DComponent>().Move(new Vector2(inputX, inputY));
            player.GetComponent<AnimatorComponent>().SetMoveParmas(inputX, inputY);
            if (inputX != 0 || inputY != 0)
            {
                player.GetComponent<AnimatorComponent>().Play(MotionType.IsMoving);
            }
            else
            {
                player.GetComponent<AnimatorComponent>().ForEveryAnimator(AnimatorControlType.ResetTrigger, MotionType.IsMoving.ToString());
            }
        }
    }
}