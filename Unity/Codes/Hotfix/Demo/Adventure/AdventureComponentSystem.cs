using System;
using UnityEngine;

namespace ET
{
    [Timer(TimerType.BattleRound)]
    public class AdventureBattleRoundTimer: ATimer<AdventureComponent>
    {
        public override void Run(AdventureComponent t)
        {
            t?.PlayOneBattleRound().Coroutine();
        }
    }

    [FriendClass(typeof(AdventureComponent))]
    public static class AdventureComponentSystem
    {
        public static void ResetAdventure(this AdventureComponent self)
        {
            for (int i = 0; i < self.EnemyIdList.Count; i++)
            {
                self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Remove(self.EnemyIdList[i]);
            }

            TimerComponent.Instance?.Remove(ref self.BattleTimer);
            self.BattleTimer = 0;
            self.Round = 0;
            self.EnemyIdList.Clear();
            self.AliveEnemyIdList.Clear();

            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
            int maxHp = unit.GetComponent<NumericComponent>().GetAsInt(NumericType.MaxHp);
            unit.GetComponent<NumericComponent>().Set(NumericType.Hp, maxHp);
            unit.GetComponent<NumericComponent>().Set(NumericType.IsAlive, 1);
            
            Game.EventSystem.Publish(new EventType.AdventureRoundReset(){ZoneScene = self.ZoneScene()});
        }
        
        public static async ETTask StartAdventure(this AdventureComponent self)
        {
            self.ResetAdventure();

            await self.CreateAdventureEnemy();
            self.ShowAdventureHpBarInfo(true);
            self.BattleTimer = TimerComponent.Instance.NewOnceTimer(500, TimerType.BattleRound, self);
        }

        public static void ShowAdventureHpBarInfo(this AdventureComponent self, bool isShow)
        {
            Unit myUnit = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene());
            Game.EventSystem.Publish(new EventType.ShowAdventureHpBar() { Unit = myUnit, isShow = isShow });
            for (int i = 0; i < self.EnemyIdList.Count; i++)
            {
                Unit enemyUnit = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);
                Game.EventSystem.Publish(new EventType.ShowAdventureHpBar() { Unit = enemyUnit, isShow = isShow });
            }
        }

        public static async ETTask CreateAdventureEnemy(this AdventureComponent self)
        {
            //根据关卡ID创建怪物
            Unit unit = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene());
            int levelId = unit.GetComponent<NumericComponent>().GetAsInt(NumericType.AdventureState);

            BattleLevelConfig config = BattleLevelConfigCategory.Instance.Get(levelId);
            for (int i = 0; i < config.MonsterIds.Length; i++)
            {
                Unit enemyUnit = await UnitFactory.CreateEnemy(self.ZoneScene().CurrentScene(), config.MonsterIds[i]);
                enemyUnit.Position = new Vector3(1.5f, -2 + i, 0);
                self.EnemyIdList.Add(enemyUnit.Id);
            }
            
            await ETTask.CompletedTask;
        }

        public static async ETTask PlayOneBattleRound(this AdventureComponent self)
        {
            Unit unit = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene());
            
            //找出所有存活的enemy
            self.GetAlivedEnemyIds();
            
            if (self.Round % 2 == 0)
            {
                //玩家回合
                Unit targetMonster = self.GetTagetMonsterUnit();
                await Game.EventSystem.PublishAsync(new EventType.AdventureBattleRound()
                {
                    ZoneScene = self.ZoneScene(), AttackUnit = unit, TargetUnit = targetMonster
                });

                await TimerComponent.Instance.WaitAsync(1000);
            }
            else
            {
                //敌方回合
                if (unit.IsAlive())
                {
                    for (int i = 0; i < self.AliveEnemyIdList.Count; i++)
                    {
                        Unit enemyUnit = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.AliveEnemyIdList[i]);
                        await Game.EventSystem.PublishAsync(new EventType.AdventureBattleRound()
                        {
                            ZoneScene = self.ZoneScene(), AttackUnit = enemyUnit, TargetUnit = unit
                        });

                        await TimerComponent.Instance.WaitAsync(1000);
                    }
                }
            }
            
            self.BattleRoundOver();
        }

        public static Unit GetTagetMonsterUnit(this AdventureComponent self)
        {
            if (self.AliveEnemyIdList.Count <= 0)
            {
                return null;
            }

            return self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.AliveEnemyIdList[0]);
        }

        public static void BattleRoundOver(this AdventureComponent self)
        {
            ++self.Round;
            var battleResult = self.GetBattleRoundResult();
            Log.Debug("当前回合结果：" + battleResult);
            switch (battleResult)
            {
                case BattleRoundResult.KeepBattle:
                    self.BattleTimer = TimerComponent.Instance.NewOnceTimer(500, TimerType.BattleRound, self);
                    break;
                case BattleRoundResult.WinBattle:
                    Unit unit = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene());
                    Game.EventSystem.PublishAsync(new EventType.AdventureBattleOver() { ZoneScene = self.ZoneScene(), WinUnit = unit }).Coroutine();
                    break;
                case BattleRoundResult.LoseBattle:
                    for (int i = 0; i < self.EnemyIdList.Count; i++)
                    {
                        Unit monsterUnit = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);
                        if (monsterUnit.IsAlive())
                        {
                            Game.EventSystem.PublishAsync(new EventType.AdventureBattleOver() { ZoneScene = self.ZoneScene(), WinUnit = monsterUnit })
                                    .Coroutine();
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Game.EventSystem.PublishAsync(new EventType.AdventureBattleReport()
                    {
                        BattleRoundResult = battleResult, Round = self.Round, ZoneScene = self.ZoneScene()
                    }).Coroutine();
        }

        public static BattleRoundResult GetBattleRoundResult(this AdventureComponent self)
        {
            Unit unit = UnitHelper.GetMyUnitFromZoneScene(self.ZoneScene());
            if (!unit.IsAlive())
            {
                return BattleRoundResult.LoseBattle;
            }
            self.GetAlivedEnemyIds();
            Unit enemyUnit = self.GetTagetMonsterUnit();
            if (enemyUnit == null)
            {
                return BattleRoundResult.WinBattle;
            }

            return BattleRoundResult.KeepBattle;
        }

        public static void GetAlivedEnemyIds(this AdventureComponent self)
        {
            self.AliveEnemyIdList.Clear();
            for (int i = 0; i < self.EnemyIdList.Count; i++)
            {
                Unit monsterUnit = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);
                if (monsterUnit.IsAlive())
                {
                    self.AliveEnemyIdList.Add(monsterUnit.Id);
                }
            }
        }
    }
}