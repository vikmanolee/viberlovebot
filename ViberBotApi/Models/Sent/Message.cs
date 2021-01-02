using System.Text.Json.Serialization;

namespace ViberBotApi.Models.Sent
{
    public class Message
    {
        public string Receiver { get; set; }

        [JsonPropertyName("min_api_version")]
        public int MinApiVersion { get; set; } = 1;

        public Sender Sender { get; set; }

        [JsonPropertyName("tracking_data")]
        public string TrackingData { get; set; }

        public virtual string Type { get; set; }
    }
}