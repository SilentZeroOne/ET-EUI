using System;

namespace ET
{
    public static class ChatHelper
    {
        public static async ETTask<int> SendChatMessage(Scene zoneScene, string chatMessage)
        {
            if (string.IsNullOrEmpty(chatMessage))
            {
                return ErrorCode.ERR_ChatMessageEmpty;
            }

            Chat2C_SendChatInfo chat2CSendChatInfo = null;
            try
            {
                chat2CSendChatInfo = (Chat2C_SendChatInfo)await zoneScene.GetComponent<SessionComponent>().Session
                        .Call(new C2Chat_SendChatInfo() { ChatMessage = chatMessage });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetworkError;
            }
            
            return chat2CSendChatInfo.Error;
        }
    }
}