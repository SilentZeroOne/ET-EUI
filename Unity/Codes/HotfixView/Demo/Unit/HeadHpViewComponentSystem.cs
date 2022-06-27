using TMPro;
using UnityEngine;

namespace ET
{
    public class HeadHpViewComponnetAwakeSystem : AwakeSystem<HeadHpViewComponent>
    {
        public override void Awake(HeadHpViewComponent self)
        {
            GameObject go = self.GetParent<Unit>().GetComponent<GameObjectComponent>().GameObject;
            self.HpBarGroup = go.GetComponent<ReferenceCollector>().GetObject("HpBarGroup") as GameObject;
            self.HpBar = (go.GetComponent<ReferenceCollector>().GetObject("HpBar") as GameObject).GetComponent<SpriteRenderer>();
            self.HpText = (go.GetComponent<ReferenceCollector>().GetObject("HpBar") as GameObject).GetComponent<TextMeshPro>();
        }
    }

    [FriendClass(typeof(HeadHpViewComponent))]
    public static class HeadHpViewComponentSystem
    {
        public static void SetVisiable(this HeadHpViewComponent self, bool isVisiable)
        {
            self.HpBarGroup?.SetActive(isVisiable);
        }

        public static void SetHp(this HeadHpViewComponent self)
        {
            NumericComponent numericComponent = self.GetParent<Unit>().GetComponent<NumericComponent>();

            int maxHp = numericComponent.GetAsInt(NumericType.MaxHp);
            int hp = numericComponent.GetAsInt(NumericType.Hp);

            self.HpText.text = $"{hp}/{maxHp}";
            self.HpBar.size = new Vector2(self.HpBar.size.x * ((float) hp / maxHp), self.HpBar.size.y);
        }
    }
}