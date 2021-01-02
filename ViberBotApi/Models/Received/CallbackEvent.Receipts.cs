using System.Text.Json.Serialization;

namespace ViberBotApi.Models.Received
{
    public partial class CallbackEvent
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}