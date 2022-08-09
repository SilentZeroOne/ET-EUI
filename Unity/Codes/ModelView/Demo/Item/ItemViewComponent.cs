using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Item))]
    public class ItemViewComponent: Entity, IAwake, IDestroy
    {
        public BoxCollider2D BoxCollider2D;

        public SpriteRenderer SpriteRenderer;
    }
}