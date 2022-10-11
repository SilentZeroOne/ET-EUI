using UnityEngine;

namespace ET
{
    public class RigidBody2DComponentAwakeSystem: AwakeSystem<RigidBody2DComponent>
    {
        public override void Awake(RigidBody2DComponent self)
        {

        }
    }

    public class RigidBody2DComponentDestroySystem: DestroySystem<RigidBody2DComponent>
    {
        public override void Destroy(RigidBody2DComponent self)
        {

        }
    }
    [FriendClassAttribute(typeof(ET.RigidBody2DComponent))]
    public static class RigidBody2DComponentSystem
    {
        public static void Move(this RigidBody2DComponent self, Vector2 movementInput)
        {
            int speed = self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt(NumericType.Speed);
            self.GetParent<Unit>().Position += (Vector3)movementInput * speed * Time.deltaTime;
            //self.Rigidbody2D.MovePosition(self.Rigidbody2D.position + movementInput * speed * Time.deltaTime);
        }
    }
}