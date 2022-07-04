﻿using TMPro;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class HeadHpViewComponent : Entity,IAwake,IDestroy
    {
        public GameObject HpBarGroup = null;
        public SpriteRenderer HpBar = null;
        public TextMeshPro HpText = null;
        public float HpBarOriginalX;
    }
}