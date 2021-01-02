using System.Text.Json.Serialization;

namespace ViberBotApi.Models.Received
{
    public partial class CallbackEvent
    {
        public Sender Sender { get; set; }

        public MessageReceived Message { get; set; }
    }

    public class Sender
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }

        [JsonPropertyName("api_version")]
        public int ApiVersion { get; set; }
    }
}