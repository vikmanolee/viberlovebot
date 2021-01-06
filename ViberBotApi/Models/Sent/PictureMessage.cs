namespace ViberBotApi.Models.Sent
{
    public class PictureMessage : Message
    {
        public override string Type { get; set; } = "picture";

        public string Text { get; set; } = "";

        // The URL must have a resource with a .jpeg, .png or (non-animated) .gif file extension as the last path segment.
        // Max image size: 1MB on iOS, 3MB on Android.
        public string Media { get; set; }

        // optional. Recommended: 400x400. Max size: 100kb.
        public string Thumbnail { get; set; }
    }
}