namespace viberlovebot.Abstractions
{
    public interface ISendMessageService
    {
        void TextMessage(string receiver, string text);
        void StickerMessage(string receiver, int stickerId);
    }
}
