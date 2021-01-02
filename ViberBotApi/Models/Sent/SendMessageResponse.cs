using System.Text.Json.Serialization;

// {"status":0,"status_message":"ok","message_token":5524744101626800773,"chat_hostname":"SN-CHAT-11_"}
namespace ViberBotApi.Models.Sent
{
    public class SendMessageResponse
    {
        public bool Success { get; set; } = true;

        public string ErrorMessage { get; set; }

        public int Status { get; set; }

        [JsonPropertyName("status_message")]
        public string StatusMessage { get; set; }

        [JsonPropertyName("message_token")]
        public long MessageToken { get; set; }

        [JsonPropertyName("chat_hostname")]
        public string ChatHostname { get; set; }
    }
}