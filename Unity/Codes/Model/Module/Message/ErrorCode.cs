namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误
        
        // 110000以下的错误请看ErrorCore.cs
        
        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常

        public const int ERR_NetworkError = 200002;
        public const int ERR_LoginInfoError = 200003;
        public const int ERR_AccountNameRegexError = 200004;
        public const int ERR_AccountPasswordRegexError = 200005;
        public const int ERR_BlacklistError = 200006;
        public const int ERR_AccountPasswordError = 200007;
        public const int ERR_RepeatedRequestError = 200008;
        public const int ERR_TokenError = 200009;
        public const int ERR_RoleNameIsNull = 200010;
        public const int ERR_RoleNameSame = 200011;
        public const int ERR_RoleNotExist = 200012;
        public const int ERR_RequestSceneTypeError = 200013;
        public const int ERR_ConnectGateKeyError = 200014;
        public const int ERR_OtherAccountLogin = 200015;
        public const int ERR_SessionPlayerError = 200016;
        public const int ERR_NonePlayerError = 200017;
        public const int ERR_SessionStateError = 200018;
        public const int ERR_EnterGameError = 200019;
        public const int ERR_ReEnterGameError = 200020;
        public const int ERR_ReEnterGameError2 = 200021;
        public const int ERR_NumericTypeNotExist = 200022;
        public const int ERR_NumericTypeNotAddPoint = 200023;
        public const int ERR_AddPointNotEnough = 200024;
        public const int ERR_AlreadyInAdventure = 200025;
        public const int ERR_AdventureInDying = 200026;
        public const int ERR_AdventureErrorLevel = 200027;
        public const int ERR_AdventureLevelNotEnough = 200028;
        public const int ERR_AdventureRoundError = 200029;
        public const int ERR_AdventureResultError = 200030;
        public const int ERR_AdventureWinResultError = 200031;
        public const int ERR_ExpNotEnough = 200032;
        public const int ERR_ExpNumError = 200033;
        public const int ERR_ItemNotExist = 200034;
        public const int ERR_AddBagItemError = 200035;
        public const int ERR_EquipItemError = 200036;
        public const int ERR_BagMaxLoad = 200037;
        public const int ERR_ProductionNotExist = 200038;
        public const int ERR_ConsumeNotEnough = 200039;
        public const int ERR_MakeConfigNotExist = 200040;
        public const int ERR_NoMakeFreeQueue = 200041;
        public const int ERR_NoMakeQueueOver = 200042;
    }
}