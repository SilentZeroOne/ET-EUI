namespace ET
{
    public class ActivityButtonComponentAwakeSystem: AwakeSystem<ActivityButtonComponent,int>
    {
        public override void Awake(ActivityButtonComponent self, int a)
        {
            self.KeyCode = self.StartCode + a;
        }
    }

    public class ActivityButtonComponentDestroySystem: DestroySystem<ActivityButtonComponent>
    {
        public override void Destroy(ActivityButtonComponent self)
        {

        }
    }

    public class ActivityButtonComponentUpdateSystem: UpdateSystem<ActivityButtonComponent>
    {
        public override void Update(ActivityButtonComponent self)
        {
            if (InputHelper.GetKeyDown(self.KeyCode))
            {
                var slot = self.GetParent<ESItemSlot>();
                slot.E_ItemEventTrigger.OnPointerClick(null);
            }
        }
    }

    [FriendClass(typeof (ActivityButtonComponent))]
    public static class ActivityButtonComponentSystem
    {
        public static void Test(this ActivityButtonComponent self)
        {
        }

        
    }
}