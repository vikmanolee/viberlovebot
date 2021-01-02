using System.Text.Json.Serialization;

namespace ViberBotApi.Models.Received
{
    public class Contact
    {
        public string Name { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        public string Avatar { get; set; }
    }
}