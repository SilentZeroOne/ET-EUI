namespace ET
{
    public class AdventureCheckComponentDestroySystem: DestroySystem<AdventureCheckComponent>
    {
        public override void Destroy(AdventureCheckComponent self)
        {
            self.ResetAdventureInfo();
        }
    }

    public static class AdventureCheckComponentSystem
    {
        public static bool CheckBattleWinResult(this AdventureCheckComponent self, int totalBattleRound)
        {
            self.ResetAdventureInfo();

            NumericComponent numericComponent = self.GetParent<Unit>().GetComponent<NumericComponent>();
            int levelId = numericComponent.GetAsInt(NumericType.AdventureState);
            //模拟战斗
            bool isSimulationNormal = self.SimulationBattle(levelId, totalBattleRound);
            if (!isSimulationNormal)
            {
                Log.Error("模拟对战失败");
                return false;
            }

            if (self.MonsterTotalDamage > numericComponent.GetAsInt(NumericType.MaxHp))
            {
                Log.Error("角色无法存活");
                return false;
            }

            if (self.MonsterTotalHp > self.UnitTotalDamage)
            {
                Log.Error("角色伤害不足");
                return false;
            }
            
            //判断战斗动画是否正常
            long playAnimationTime = TimeHelper.ServerNow() - numericComponent.GetAsLong(NumericType.AdventureStartTime);
            if (playAnimationTime < self.AnimationTotalTime)
            {
                Log.Error("动画时间不足");
                return false;
            }

            return true;
        }

        public static bool SimulationBattle(this AdventureCheckComponent self, int levelId, int totalBattleRound)
        {
            //创建怪物信息
            BattleLevelConfig config = BattleLevelConfigCategory.Instance.Get(levelId);
            for (int i = 0; i < config.MonsterIds.Length; i++)
            {
                UnitConfig unitConfig = UnitConfigCategory.Instance.Get(config.MonsterIds[i]);
                self.EnemyHpDict.Add(i, unitConfig.MaxHP);
                self.MonsterTotalHp += unitConfig.MaxHP; 
            }
            
            //开始模拟战斗
            for (int i = 0; i < totalBattleRound; i++)
            {
                if (self.Round % 2 == 0)
                {
                    //玩家回合
                    int targetIndex = self.GetFirstAliveMonsterIndex(levelId);
                    if (targetIndex < 0)
                    {
                        Log.Error($"TargeIndex error :{targetIndex}");
                        return false;
                    }

                    int damage = self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt(NumericType.DamageValue);
                    self.EnemyHpDict[targetIndex] -= damage;
                    self.UnitTotalDamage += damage;
                    self.AnimationTotalTime += 1000;
                }
                else
                {
                    //敌方回合
                    for (int j = 0; j < config.MonsterIds.Length; j++)
                    {
                        if (self.EnemyHpDict[i] < 0)
                        {
                            continue;
                        }

                        self.MonsterTotalDamage += UnitConfigCategory.Instance.Get(config.MonsterIds[j]).DamageValue;
                        self.AnimationTotalTime += 1000;
                    }
                }

                ++self.Round;
            }

            return true;
        }

        public static void ResetAdventureInfo(this AdventureCheckComponent self)
        {
            self.Round = 0;
            self.AnimationTotalTime = 0;
            self.MonsterTotalDamage = 0;
            self.MonsterTotalHp = 0;
            self.UnitTotalDamage = 0;
            self.EnemyHpDict.Clear();
        }

        public static int GetFirstAliveMonsterIndex(this AdventureCheckComponent self,int levelId)
        {
            BattleLevelConfig config = BattleLevelConfigCategory.Instance.Get(levelId);
            for (int i = 0; i < config.MonsterIds.Length; i++)
            {
                if (self.EnemyHpDict[i] > 0)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}