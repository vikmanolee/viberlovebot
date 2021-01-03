using System.Text.Json.Serialization;

namespace ViberBotApi.Models.Received
{
    public partial class CallbackEvent
    {
        // "open" for now
        [JsonPropertyName("type")]
        public string ConversationStartedType { get; set; }
        public string Context { get; set; }
        public bool Subscribed { get; set; }
    }
}