﻿using UnityEngine;

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

            self.BattleTimer = TimerComponent.Instance.NewOnceTimer(500, TimerType.BattleRound, self);
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
            await ETTask.CompletedTask;
        }
    }
}