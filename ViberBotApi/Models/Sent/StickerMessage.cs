using System.Text.Json.Serialization;

namespace ViberBotApi.Models.Sent
{
    public class StickerMessage : Message
    {
        public override string Type { get; set; } = "sticker";

        [JsonPropertyName("sticker_id")]
        public int StickerId { get; set; }
    }
}