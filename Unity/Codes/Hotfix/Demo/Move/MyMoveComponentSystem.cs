namespace ET
{
    public class MyMoveComponentAwakeSystem: AwakeSystem<MyMoveComponent>
    {
        public override void Awake(MyMoveComponent self)
        {

        }
    }

    public class MyMoveComponentDestroySystem: DestroySystem<MyMoveComponent>
    {
        public override void Destroy(MyMoveComponent self)
        {

        }
    }
    
    public class MyMoveComponentUpdateSystem: UpdateSystem<MyMoveComponent>
    {
        public override void Update(MyMoveComponent self)
        {
            
        }
    }
    
    public static class MyMoveComponentSystem
    {
        public static void Update(this MyMoveComponent self)
        {
            
        }
    }
}