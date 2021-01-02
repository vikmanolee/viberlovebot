using System;
using ViberBotApi.Models;
using ViberBotApi.Models.Sent;
using ViberBotApi.Models.Received;

namespace ViberBotApi
{
    public class ViberBotWebhookHandler
    {
        public Func<CallbackEvent, TextMessage> conversationStartedHandler;
        public Action<CallbackEvent> receivedMessageHandler;

        public TextMessage HandleEvent(CallbackEvent message)
        {
            switch (message.EventType)
            {
                case CallbackEventTypes.ConversationStarted:
                    return conversationStartedHandler(message);
                case CallbackEventTypes.Message:
                    receivedMessageHandler(message);
                    return null;
                default:
                    return null;
            }
        }
    }
}