namespace ViberBotApi.Models.Sent
{
    public class TextMessage : Message
    {
        public override string Type { get; set; } = "text";

        public string Text { get; set; }
    }
}