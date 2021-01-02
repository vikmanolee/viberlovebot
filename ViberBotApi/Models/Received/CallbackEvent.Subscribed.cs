using System.Text.Json.Serialization;

namespace ViberBotApi.Models.Received
{
    public partial class CallbackEvent
    {
        public User User { get; set; }
    }

    public class User
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