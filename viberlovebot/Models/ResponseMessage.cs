using viberlovebot.Enumerations;

namespace viberlovebot.Models
{
    public class ResponseMessage
    {
        public ResponseMessageType Type { get; set; }

        public string Text { get; set; }

        public int StickerId { get; set; }

        public string MediaUrl { get; set; }
    }
}