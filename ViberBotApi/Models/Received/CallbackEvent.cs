using System.Text.Json.Serialization;

namespace ViberBotApi.Models.Received
{
    public partial class CallbackEvent
    {
        [JsonPropertyName("event")]
        public string EventType { get; set; }

        public long Timestamp { get; set; }

        [JsonPropertyName("message_token")]
        public long MessageToken { get; set; }
    }
}