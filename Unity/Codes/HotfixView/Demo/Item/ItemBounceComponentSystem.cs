using UnityEngine;

namespace ET
{
    [Timer(TimerType.ItemBounceTimer)]
    public class ItemBounceTimerTimer: ATimer<ItemBounceComponent>
    {
        public override void Run(ItemBounceComponent self)
        {
            self.MoveForward();
        }
    }
    
    public class ItemBounceComponentAwakeSystem: AwakeSystem<ItemBounceComponent,Vector3,Vector3>
    {
        public override void Awake(ItemBounceComponent self, Vector3 targetPos,Vector3 startPos)
        {
            self.TargetPos = targetPos;
            self.StartPos = startPos;
            self.Init();
        }
    }
    [FriendClassAttribute(typeof(ET.ItemViewComponent))]
    public class ItemBounceComponentDestroySystem : DestroySystem<ItemBounceComponent>
    {
        public override void Destroy(ItemBounceComponent self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
            self.ShadowRenderer.gameObject.SetActive(false);
            self.GetParent<Item>().GetComponent<ItemViewComponent>().BoxCollider2D.enabled = true;
        }
    }

    [FriendClass(typeof(ItemBounceComponent))]
    [FriendClass(typeof(ItemViewComponent))]
    public static class ItemBounceComponentSystem
    {
        public static void Init(this ItemBounceComponent self)
        {
            var itemObj = self.GetParent<Item>().GetComponent<GameObjectComponent>().GameObject;
            self.ItemTransform = itemObj.transform;
            self.ShadowRenderer = itemObj.GetComponentFormRC<SpriteRenderer>("Shadow");
            var viewComponent = self.GetParent<Item>().GetComponent<ItemViewComponent>();

            self.ShadowRenderer.sprite = viewComponent.SpriteRenderer.sprite;
            self.ShadowRenderer.color = new Color(0, 0, 0, 0.3f);
            self.ShadowRenderer.gameObject.SetActive(true);
            
            viewComponent.BoxCollider2D.enabled = false;
            
            self.Direction = (self.TargetPos - self.StartPos).normalized;

            self.ItemTransform.position = self.StartPos;
            viewComponent.SpriteRenderer.transform.position += Vector3.up * 1.5f;
            self.Distance = Vector3.Distance(self.StartPos, self.TargetPos);
        }

        public static async ETTask MoveAsync(this ItemBounceComponent self)
        {
            ETTask tsc = ETTask.Create(true);
            self.CallBack = () => { tsc.SetResult(); };

            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerType.ItemBounceTimer, self);
            
            await tsc;
        }

        public static void MoveForward(this ItemBounceComponent self)
        {
            var viewComponent = self.GetParent<Item>().GetComponent<ItemViewComponent>();
            self.IsGround = viewComponent.SpriteRenderer.transform.position.y <= self.ItemTransform.position.y;

            if (Vector3.Distance(self.ItemTransform.position, self.TargetPos) > 0.1f)
            {
                self.ItemTransform.position += (Vector3)self.Direction * self.Distance * -Settings.ItemBounceGravity * Time.deltaTime;
            }

            if (!self.IsGround)
            {
                viewComponent.SpriteRenderer.transform.position += Vector3.up * Settings.ItemBounceGravity * Time.deltaTime;
            }
            else
            {
                var callBack = self.CallBack;
                self.CallBack = null;
                
                callBack?.Invoke();
            }
        }
    }
}