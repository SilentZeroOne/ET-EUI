namespace ET
{
    public class AdventureCheckComponentDestroySystem: DestroySystem<AdventureCheckComponent>
    {
        public override void Destroy(AdventureCheckComponent self)
        {
            foreach (var monsterId in self.CacheEnemyIdList)
            {
                self.DomainScene().GetComponent<UnitComponent>().Remove(monsterId);
            }
            self.EnemyIdList.Clear();
            self.CacheEnemyIdList.Clear();
            self.Random = null;
            self.AnimationTotalTime = 0;
        }
    }

    [FriendClass(typeof(AdventureCheckComponent))]
    [FriendClass(typeof(Unit))]
    public static class AdventureCheckComponentSystem
    {
        public static bool CheckBattleWinResult(this AdventureCheckComponent self, int totalBattleRound)
        {
            try
            {
                self.ResetAdventureInfo();
                self.SetBattleRandomSeed();
                self.CreateBattleMonsterUnit();

                NumericComponent numericComponent = self.GetParent<Unit>().GetComponent<NumericComponent>();
                int levelId = numericComponent.GetAsInt(NumericType.AdventureState);
                //模拟战斗
                bool isSimulationNormal = self.SimulationBattle(levelId, totalBattleRound);
                if (!isSimulationNormal)
                {
                    Log.Error("模拟对战失败");
                    return false;
                }

                if (!self.GetParent<Unit>().IsAlive())
                {
                    Log.Error("玩家死亡");
                    return false;
                }

                if (self.GetFirstAliveMonster() != null)
                {
                    Log.Error("模拟对战失败,怪物存活");
                    return false;
                }

                //判断战斗动画是否正常
                long playAnimationTime = TimeHelper.ServerNow() - numericComponent.GetAsLong(NumericType.AdventureStartTime);
                if (playAnimationTime < self.AnimationTotalTime)
                {
                    Log.Error($"动画时间不足 实际：{playAnimationTime}ms 模拟：{self.AnimationTotalTime}ms");
                    return false;
                }

                return true;
            }
            finally
            {
                self.ResetAdventureInfo();
            }
        }

        public static bool SimulationBattle(this AdventureCheckComponent self, int levelId, int totalBattleRound)
        {
            //开始模拟战斗
            for (int i = 0; i < totalBattleRound; i++)
            {
                if (i % 2 == 0)
                {
                    //玩家回合
                    Unit targetMonster = self.GetFirstAliveMonster();
                    if (targetMonster == null)
                    {
                        Log.Debug("Monster is null");
                        return false;
                    }

                    self.AnimationTotalTime += 1000;
                    self.CaculateDamageValue(self.GetParent<Unit>(), targetMonster);
                }
                else
                {
                    if (!self.GetParent<Unit>().IsAlive())
                    {
                        return false;
                    }
                    
                    //敌方回合
                    for (int j = 0; j < self.EnemyIdList.Count; j++)
                    {
                        Unit monsterUnit = self.DomainScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[j]);
                        if (!monsterUnit.IsAlive())
                        {
                            continue;
                        }
                        
                        self.AnimationTotalTime += 1000;
                        self.CaculateDamageValue(monsterUnit,self.GetParent<Unit>());
                    }
                }
                
            }

            return true;
        }

        public static void ResetAdventureInfo(this AdventureCheckComponent self)
        {
            self.AnimationTotalTime = 0;
            NumericComponent numericComponent = self.GetParent<Unit>().GetComponent<NumericComponent>();
            numericComponent.SetNoEvent(NumericType.Hp,numericComponent.GetAsInt(NumericType.MaxHp));
            numericComponent.SetNoEvent(NumericType.IsAlive, 1);
        }

        public static Unit GetFirstAliveMonster(this AdventureCheckComponent self)
        {
            for (int i = 0; i < self.EnemyIdList.Count; i++)
            {
                Unit monster = self.DomainScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);
                if (monster.IsAlive())
                {
                    return monster;
                }
            }

            return null;
        }

        public static void SetBattleRandomSeed(this AdventureCheckComponent self)
        {
            int seed = self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt(NumericType.BattleRandomSeed);
            if (self.Random == null)
            {
                self.Random = new SRandom((uint)seed);
            }
            else
            {
                self.Random.SetRandomSeed((uint)seed);
            }
        }

        public static void CreateBattleMonsterUnit(this AdventureCheckComponent self)
        {
            int level = self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt(NumericType.AdventureState);
            var battleLevelConfig = BattleLevelConfigCategory.Instance.Get(level);
            var monsterCount = battleLevelConfig.MonsterIds.Length - self.CacheEnemyIdList.Count;
            for (int i = 0; i < monsterCount; i++)
            {
                Unit monsterUnit = UnitFactory.CreateEnemy(self.DomainScene(), 1002);
                self.CacheEnemyIdList.Add(monsterUnit.Id);
            }
            
            //复用怪物Unit
            self.EnemyIdList.Clear();
            for (int i = 0; i < battleLevelConfig.MonsterIds.Length; i++)
            {
                Unit monster = self.DomainScene().GetComponent<UnitComponent>().Get(self.CacheEnemyIdList[i]);
                UnitConfig config = UnitConfigCategory.Instance.Get(battleLevelConfig.MonsterIds[i]);
                monster.ConfigId = config.Id;
                
                NumericComponent numericComponent = monster.GetComponent<NumericComponent>();
                numericComponent.SetNoEvent(NumericType.IsAlive, 1);
                numericComponent.SetNoEvent(NumericType.DamageValue, monster.Config.DamageValue);
                numericComponent.SetNoEvent(NumericType.MaxHp, monster.Config.MaxHP);
                numericComponent.SetNoEvent(NumericType.Hp, monster.Config.MaxHP);
                self.EnemyIdList.Add(monster.Id);
            }
        }

        public static void CaculateDamageValue(this AdventureCheckComponent self, Unit unit, Unit targetUnit)
        {
            var numericComponent = targetUnit.GetComponent<NumericComponent>();
            int targetHp = numericComponent.GetAsInt(NumericType.Hp);
            targetHp -= DamageCaculateHelper.CaculateDamageValue(unit, targetUnit, ref self.Random);
            if (targetHp <= 0)
            {
                targetHp = 0;
                numericComponent.SetNoEvent(NumericType.IsAlive, 0);
            }

            numericComponent.SetNoEvent(NumericType.Hp, targetHp);
        }
    }
}