using System.Text.Json.Serialization;

namespace ViberBotApi.Models.Received
{
    public partial class MessageReceived
    {
        public string Type { get; set; }

        [JsonPropertyName("tracking_data")]
        public string TrackingData { get; set; }

        public string Text { get; set; }

        // Relevant for picture/video/file/url type messages
        public string Media { get; set; }

        public Location Location { get; set; }

        // Relevant for contact type messages
        public Contact Contact { get; set; }

        // Relevant for file type messages
        [JsonPropertyName("file_name")]
        public string FileName { get; set; }

        // Relevant for file type messages
        [JsonPropertyName("file_size")]
        public int FileSize { get; set; }

        // Relevant for video type messages
        public int Duration { get; set; }

        // Relevant for sticker type messages
        [JsonPropertyName("sticker_id")]
        public int StickerId { get; set; }
    }
}