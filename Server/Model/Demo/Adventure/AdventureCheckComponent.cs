﻿using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class AdventureCheckComponent : Entity, IAwake,IDestroy
    {
        public Dictionary<int, int> EnemyHpDict = new Dictionary<int, int>();
        public int Round = 0;
        public int AnimationTotalTime = 0;
        public int MonsterTotalDamage = 0;
        public int UnitTotalDamage = 0;
        public int MonsterTotalHp = 0;
    }
}