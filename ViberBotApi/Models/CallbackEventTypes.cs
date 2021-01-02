namespace ViberBotApi.Models
{
    public static class CallbackEventTypes
    {
        public const string Message = "message";
        public const string ConversationStarted = "conversation_started";
        public const string Delivered = "delivered";
        public const string Seen = "seen";
        public const string Failed = "failed";
        public const string Subscribed = "subscribed";
        public const string Unsubscribed = "unsubscribed";
    }
}