namespace viberlovebot.Services
{
    public interface ISendMessageService
    {
        void TextMessage(string receiver, string text);
        void StickerMessage(string receiver, int stickerId);
    }
}
