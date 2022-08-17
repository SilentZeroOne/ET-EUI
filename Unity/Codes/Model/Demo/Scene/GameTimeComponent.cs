namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class GameTimeComponent: Entity, IAwake, IDestroy
    {
        public int GameSecond;
        public int GameMinute;
        public int GameHour;
        public int GameDay;
        public int GameMonth;
        public int GameYear;

        public int MonthInSeason;
#if !NOT_UNITY
        public Season Season;
#endif
        
    }
}