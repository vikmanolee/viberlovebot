using viberlovebot.Models;

namespace viberlovebot.Abstractions
{
    public interface ISendMessageService
    {
        void SendResponse(ResponseMessage response, string receiverId);
        void TextMessage(string receiver, string text);
        void StickerMessage(string receiver, int stickerId);
        void UrlMessage(string receiver, string url);
        void PictureMessage(string receiver, string pictureUrl, string description);
    }
}
