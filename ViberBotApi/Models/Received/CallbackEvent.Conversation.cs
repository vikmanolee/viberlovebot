using System.Text.Json.Serialization;

namespace ViberBotApi.Models.Received
{
    public partial class CallbackEvent
    {
        public string Type { get; set; }
        public string Context { get; set; }
        public bool Subscribed { get; set; }
    }
}