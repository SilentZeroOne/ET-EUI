using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Crop))]
    public class CropViewComponent: Entity, IAwake, IDestroy
    {
        public BoxCollider2D BoxCollider2D;

        public SpriteRenderer SpriteRenderer;
    }
}