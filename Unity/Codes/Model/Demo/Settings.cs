namespace ET
{
    public static class Settings
    {
        public const float FadeDuration = 0.3f;
        public const float FadeAlpha = 0.45f;
        public const int MaxActionBarSlot = 10;
        public const float ItemBounceGravity = -7;

        //时间相关
        public const int DefaultYear = 2022;
        public const float SecondThreshold = 0.1f;  //数值越小 时间越快(不能小于0.1)
        public const int SecondHold = 59;
        public const int MinuteHold = 59;
        public const int HourHold = 23;
        public const int DayHold = 10;   //几天一个月
        public const int SeasonHold = 3;
        public const int MonthHold = 12;

        public const int GridSize = 1;
        public const float GridDiagonalSize = 1.41f;

    }
}