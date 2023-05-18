namespace ET
{
    namespace WaitType
    {
        public struct Wait_UnitStop: IWaitType
        {
            public int Error
            {
                get;
                set;
            }
        }
        
        public struct Wait_CreateMyUnit: IWaitType
        {
            public int Error
            {
                get;
                set;
            }

            public M2C_CreateMyUnit Message;
        }
        
        public struct Wait_SceneChangeFinish: IWaitType
        {
            public int Error
            {
                get;
                set;
            }
        }
        
        public struct Wait_PlayRoomAddObjectComponentFinish: IWaitType
        {
            public int Error
            {
                get;
                set;
            }
        }
        
        public struct Wait_MainUILoad: IWaitType
        {
            public int Error
            {
                get;
                set;
            }
        }
    }
}