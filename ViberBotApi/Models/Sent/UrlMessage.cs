namespace ViberBotApi.Models.Sent
{
    public class UrlMessage : Message
    {
        public override string Type { get; set; } = "url";

        // URL required. Max 2,000 characters
        public string Media { get; set; }
    }
}