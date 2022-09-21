﻿namespace ET
{
    [Timer(TimerType.GameTimer)]
    public class GameTimer: ATimer<GameTimeComponent>
    {
        public override void Run(GameTimeComponent self)
        {
            self.UpdateTime();
        }
    }

    public class GameTimeComponentAwakeSystem: AwakeSystem<GameTimeComponent>
    {
        public override void Awake(GameTimeComponent self)
        {
            self.GameSecond = 0;
            self.GameMinute = 0;
            self.GameHour = 0;
            self.GameDay = 1;
            self.GameMonth = 1;
            self.MonthInSeason = 2;

            //每秒执行
            self.GameYear = Settings.DefaultYear;
            self.Season = Season.Winter;
            TimerComponent.Instance.NewRepeatedTimer((long)(Settings.SecondThreshold * 1000), TimerType.GameTimer, self);
        }
    }

    public class GameTimeComponentDestroySystem: DestroySystem<GameTimeComponent>
    {
        public override void Destroy(GameTimeComponent self)
        {
        }
    }

    [FriendClass(typeof (GameTimeComponent))]
    public static class GameTimeComponentSystem
    {
        public static void UpdateTime(this GameTimeComponent self)
        {
            self.GameSecond++;
            if (self.GameSecond > Settings.SecondHold)
            {
                self.GameSecond = 0;
                self.GameMinute++;

                Game.EventSystem.Publish(new EventType.UpdateGameMinute() { ZoneScene = self.ZoneScene(), Time = self });

                if (self.GameMinute > Settings.MinuteHold)
                {
                    self.GameMinute = 0;
                    self.GameHour++;

                    Game.EventSystem.Publish(new EventType.UpdateGameHour() { ZoneScene = self.ZoneScene(), Time = self });

                    if (self.GameHour > Settings.HourHold)
                    {
                        self.GameHour = 0;
                        self.GameDay++;

                        Game.EventSystem.Publish(new EventType.UpdateGameDay() { ZoneScene = self.ZoneScene(), Time = self });

                        if (self.GameDay > Settings.DayHold)
                        {
                            self.GameDay = 1;
                            self.GameMonth++;

                            if (self.GameMonth > Settings.MonthHold)
                            {
                                self.GameMonth = 1;
                                self.GameYear++;

                                if (self.GameYear > 9999)
                                {
                                    self.GameYear = Settings.DefaultYear;
                                }
                            }

                            self.MonthInSeason--;
                            if (self.MonthInSeason == 0)
                            {
                                self.MonthInSeason = Settings.SeasonHold;
                                var currentSeason = (int)self.Season;
                                currentSeason++;
                                if (currentSeason > 3)
                                {
                                    currentSeason = 0;
                                }

                                self.Season = (Season)currentSeason;
                                Game.EventSystem.Publish(new EventType.UpdateGameSeason() { ZoneScene = self.ZoneScene(), Time = self });
                            }
                        }
                    }
                }
            }

            Game.EventSystem.Publish(new EventType.UpdateGameSecond() { ZoneScene = self.ZoneScene(), Time = self });
            //Log.Info("Current Time "+$"{self.GameHour:00}:{self.GameMinute:00}:{self.GameSecond:00}");
        }

        /// <summary>
        /// 飞跃一天
        /// </summary>
        /// <param name="self"></param>
        public static void FlashDay(this GameTimeComponent self)
        {
            self.GameDay++;
            Game.EventSystem.Publish(new EventType.UpdateGameDay() { ZoneScene = self.ZoneScene(), Time = self });
            if (self.GameDay > Settings.DayHold)
            {
                self.GameDay = 1;
                self.GameMonth++;

                if (self.GameMonth > Settings.MonthHold)
                {
                    self.GameMonth = 1;
                    self.GameYear++;

                    if (self.GameYear > 9999)
                    {
                        self.GameYear = Settings.DefaultYear;
                    }
                }

                self.MonthInSeason--;
                if (self.MonthInSeason == 0)
                {
                    self.MonthInSeason = Settings.SeasonHold;
                    var currentSeason = (int)self.Season;
                    currentSeason++;
                    if (currentSeason > 3)
                    {
                        currentSeason = 0;
                    }

                    self.Season = (Season)currentSeason;
                    Game.EventSystem.Publish(new EventType.UpdateGameSeason() { ZoneScene = self.ZoneScene(), Time = self });
                }
            }
        }
    }
}